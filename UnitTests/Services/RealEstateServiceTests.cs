namespace UnitTests.Services;

public class RealEstateServiceTests
{
    [Fact]
    public async Task add_real_estate_should_invoke_add_real_estate_repository()
    {
        // Arrange
        var realEstateRepositoryMock = new Mock<IRealEstateRepository>();
        var mapperMock = new Mock<IMapper>();

        var realEstateService = new RealEstateService(realEstateRepositoryMock.Object, mapperMock.Object);

        var realEstateDto = new CreateRealEstateDto()
        {
            Street = "Test street",
            StreetNumber = "12345",
            PostCode = "12-345",
            City = "Test city"
        };

        mapperMock.Setup(x => x.Map<RealEstate>(realEstateDto)).Returns(new RealEstate()
        {
            Street = realEstateDto.Street,
            StreetNumber = realEstateDto.StreetNumber,
            PostCode = realEstateDto.PostCode,
            City = realEstateDto.City,
        });

        // Act
        await realEstateService.AddAsync(realEstateDto, "c9351c9e-e763-4784-b9ca-66dd31e27419");

        // Assert
        realEstateRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RealEstate>()), Times.Once);
    }

}
