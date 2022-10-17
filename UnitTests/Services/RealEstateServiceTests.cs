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

    [Fact]
    public async Task when_invoking_get_real_estate_by_id_should_invoke_get_real_estate_by_id_repository()
    {
        // Arrange
        var realEstateRepositoryMock = new Mock<IRealEstateRepository>();
        var mapperMock = new Mock<IMapper>();

        var realEstateService = new RealEstateService(realEstateRepositoryMock.Object, mapperMock.Object);

        var realEstate = new RealEstate()
        {
            Id = 1,
            Street = "Test street",
            StreetNumber = "12345",
            PostCode = "12-345",
            City = "Test city"
        };

        var realEstateDto = new RealEstateDto()
        {
            Id = realEstate.Id,
            Street = realEstate.Street,
            StreetNumber = realEstate.StreetNumber,
            PostCode = realEstate.PostCode,
            City = realEstate.City,
        };

        mapperMock.Setup(x => x.Map<RealEstate>(realEstateDto)).Returns(realEstate);
        realEstateRepositoryMock.Setup(x => x.GetByIdAsync(realEstate.Id)).ReturnsAsync(realEstate);

        // Act
        var existingRealEstateDto = await realEstateService.GetByIdAsync(realEstate.Id);

        // Assert
        realEstateRepositoryMock.Verify(x => x.GetByIdAsync(realEstate.Id), Times.Once);
        realEstateDto.Should().NotBeNull();
        realEstateDto.Street.Should().NotBeNull();
        realEstateDto.StreetNumber.Should().NotBeNull();
        realEstateDto.PostCode.Should().NotBeNull();
        realEstateDto.City.Should().NotBeNull();
        realEstateDto.Street.Should().BeEquivalentTo(realEstate.Street);
        realEstateDto.StreetNumber.Should().BeEquivalentTo(realEstate.StreetNumber);
        realEstateDto.PostCode.Should().BeEquivalentTo(realEstate.PostCode);
        realEstateDto.City.Should().BeEquivalentTo(realEstate.City);
    }
}
