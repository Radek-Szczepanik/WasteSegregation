namespace WasteSegregation.Domain.Entities;

public class RealEstate : AuditableEntity
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }

    public List<RealEstateWaste> RealEstateWastes { get; set; }
}
