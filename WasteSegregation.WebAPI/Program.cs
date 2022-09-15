var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    ConfigurationManager configuration = builder.Configuration;

    builder.Services.AddSingleton(MappingsProfile.Initialize());

    builder.Services.AddControllers();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddScoped<WasteSegregationSeeder>();

    builder.Services.AddDbContext<WasteSegregationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WasteSegregationDbConnection"),
        x => x.MigrationsAssembly("WasteSegregation.WebAPI")));

    builder.Services.AddScoped<IRealEstateService, RealEstateService>();

    builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<WasteSegregationSeeder>();

    // Configure the HTTP request pipeline.

    seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

