namespace IntegrationTests;

public class RealEstateControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient client;
    private WebApplicationFactory<Program> factory;

    public RealEstateControllerTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory
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
                });
        this.client = this.factory.CreateClient();
    }

    private void SeedRealEstates(RealEstate realEstate)
    {
        var scopeFactory = this.factory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<WasteSegregationDbContext>();
        dbContext.RealEstates.Add(realEstate);
        dbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateRealEstate_WithValidModel_ReturnsCreatedStatus()
    {
        // Arrange
        var model = new CreateRealEstateDto()
        {
            Street = "TestStreet",
            StreetNumber = "12345",
            PostCode = "12-345",
            City = "TestCity"
        };

        var httpContent = model.ToJsonHttpContent();

        // Act
        var response = await this.client.PostAsync("/api/RealEstates", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Should().NotBeNull();
    }

    [Fact]
    public async Task Delete_ForRealEstateOwner_ReturnsNoContentStatus()
    {
        // Arrange
        var realEstate = new RealEstate()
        {
            UserId = "1",
            Street = "TestStreet"
        };

        SeedRealEstates(realEstate);

        // Act
        var response = await this.client.DeleteAsync("/api/RealEstates/" + realEstate.Id);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ForNonRealEstateOwner_ReturnsBadRequestStatus()
    {
        // Arrange
        var realEstate = new RealEstate()
        {
            UserId = "329",
            Street = "TestStreet"
        };

        SeedRealEstates(realEstate);

        // Act
        var response = await this.client.DeleteAsync("/api/RealEstates/" + realEstate.Id);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ForNonExistingRealEstate_ReturnsNotFoundStatus()
    {
        // Arrange


        // Act
        var response = await this.client.DeleteAsync("/api/RealEstates/555");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateRealEstate_WithInvalidModel_ReturnsBadRequestStatus()
    {
        // Arrange
        var model = new CreateRealEstateDto()
        {
            Street = "",
            StreetNumber = "12345",
            PostCode = "12-345",
            City = "TestCity"
        };

        var httpContent = model.ToJsonHttpContent();

        // Act
        var response = await this.client.PostAsync("/api/RealEstates", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
