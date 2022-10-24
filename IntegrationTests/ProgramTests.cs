namespace IntegrationTests;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly List<Type> controllerTypes;
    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        this.controllerTypes = typeof(Program)
            .Assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
            .ToList();

        this.factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                this.controllerTypes.ForEach(c => services.AddScoped(c));
            });
        });
    }

    [Fact]
    public void ConfigureServices_ForControllers_RegistersAllDependencies()
    {
        var scopeFactory = this.factory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();

        // Assert
        this.controllerTypes.ForEach(t =>
        {
            var controller = scope.ServiceProvider.GetService(t);
            controller.Should().NotBeNull();
        });
    }
}
