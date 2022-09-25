var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    ConfigurationManager configuration = builder.Configuration;

    builder.Services.AddControllers();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddAuthorization();
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<WasteSegregationDbContext>()
                    .AddDefaultTokenProviders();
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
    builder.Services.AddScoped<IRealEstateService, RealEstateService>();
    builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
    builder.Services.AddScoped<IWasteBagsService, WasteBagsService>();
    builder.Services.AddScoped<IWasteBagsRepository, WasteBagsRepository>();
    builder.Services.AddSingleton(MappingsProfile.Initialize());
    builder.Services.AddDbContext<WasteSegregationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WasteSegregationDbConnection"),
        x => x.MigrationsAssembly("WasteSegregation.WebAPI")));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "JWT Authentication",
            Description = "Enter JWT Bearer token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {securityScheme, new string[] {} }
        });
    });
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddScoped<WasteSegregationSeeder>();

    // Configure the HTTP request pipeline.

    var app = builder.Build();
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<WasteSegregationSeeder>();

    seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
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

