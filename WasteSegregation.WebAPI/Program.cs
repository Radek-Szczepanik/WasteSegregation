using Microsoft.AspNetCore.Identity;
using WasteSegregation.Infrastructure.Identity;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    ConfigurationManager configuration = builder.Configuration;

    builder.Services.AddSingleton(MappingsProfile.Initialize());

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<WasteSegregationDbContext>()
        .AddDefaultTokenProviders();


    builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddScoped<WasteSegregationSeeder>();

    builder.Services.AddDbContext<WasteSegregationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WasteSegregationDbConnection"),
        x => x.MigrationsAssembly("WasteSegregation.WebAPI")));

    builder.Services.AddScoped<IRealEstateService, RealEstateService>();

    builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();

    builder.Services.AddScoped<IWasteBagsService, WasteBagsService>();

    builder.Services.AddScoped<IWasteBagsRepository, WasteBagsRepository>();

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

