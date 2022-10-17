namespace IntegrationTests;

public class RealEstateControllerTests
{
    [Theory]
    [InlineData("pageSize=5&pageNumber=1")]
    [InlineData("pageSize=15&pageNumber=3")]
    [InlineData("pageSize=125&pageNumber=1")]
    public async Task GetAllAsync_WithQueryParameters_ReturnsOkResult(string queryParams)
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

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
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/RealEstates?" + queryParams);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
