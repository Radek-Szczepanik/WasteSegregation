var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddScoped<WasteSegregationSeeder>();

builder.Services.AddDbContext<WasteSegregationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("WasteSegregationDbConnection"),
    x => x.MigrationsAssembly("WasteSegregation.WebAPI")));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
