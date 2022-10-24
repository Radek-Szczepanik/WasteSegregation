namespace IntegrationTests;

public class IdentityControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient client;
    public IdentityControllerTests(WebApplicationFactory<Program> factory)
    {
        this.client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service =>
                        service.ServiceType == typeof(DbContextOptions<WasteSegregationDbContext>));

                    services.Remove(dbContextOptions);

                    services.AddDbContext<WasteSegregationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
                });
            })
        .CreateClient();
    }

    [Fact]
    public async Task RegisterUser_ForValidModel_ReturnsOkStatus()
    {
        // Arrange
        var registerUser = new RegisterModel()
        {
            Username = "username",
            Email = "email@test.com",
            Password = "!Password123"
        };

        var httpContent = registerUser.ToJsonHttpContent();

        // Act
        var response = await this.client.PostAsync("api/identity/register", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
