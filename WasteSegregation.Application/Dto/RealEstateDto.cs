namespace WasteSegregation.Application.Dto;

public class RealEstateDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }

    public List<RealEstateWasteDto> wasteDtos { get; set; }
}
