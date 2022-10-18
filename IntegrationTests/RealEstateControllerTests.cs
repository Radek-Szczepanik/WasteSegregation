namespace IntegrationTests;

public class RealEstateControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient client;
    public RealEstateControllerTests(WebApplicationFactory<Program> factory)
    {
        this.client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => 
                            service.ServiceType == typeof(DbContextOptions<WasteSegregationDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<WasteSegregationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
                    });
                })
                .CreateClient();
    }

    [Fact]
    public async Task CreateRealEstate_WithWalidModel_ReturnsCreatedStatus()
    {
        // Arrange
        var model = new CreateRealEstateDto()
        {
            Street = "TestStreet",
            StreetNumber = "12345",
            PostCode = "12-345",
            City = "TestCity"
        };

        var jsonModel = JsonConvert.SerializeObject(model);

        var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

        // Act
        var response = await this.client.PostAsync("/api/RealEstates", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Should().NotBeNull();
    }

    [Theory]
    [InlineData("pageSize=5&pageNumber=1")]
    [InlineData("pageSize=15&pageNumber=3")]
    [InlineData("pageSize=125&pageNumber=1")]
    public async Task GetAllAsync_WithQueryParameters_ReturnsOkResult(string queryParams)
    {
        // Arrange
        

        // Act
        var response = await client.GetAsync("/api/RealEstates?" + queryParams);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("pageSize=abc&pageNumber=1")]
    [InlineData("pageSize=15&pageNumber=xyz")]   
    public async Task GetAllAsync_WithInvalidQueryParameters_ReturnsBadRequestResult(string queryParams)
    {
        // Arrange
        

        // Act
        var response = await client.GetAsync("/api/RealEstates?" + queryParams);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
