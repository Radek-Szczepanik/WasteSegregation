namespace WasteSegregation.Application.Dto;

public class RealEstateDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    
    public List<WasteBagDto> wasteBags { get; set; } = new List<WasteBagDto>();
}
