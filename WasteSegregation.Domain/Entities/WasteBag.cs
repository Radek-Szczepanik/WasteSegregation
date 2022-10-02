namespace WasteSegregation.Domain.Entities;

public class WasteBag
{
    public int Id { get; set; }
    public int? BlueBag { get; set; }
    public int? GreenBag { get; set; }
    public int? YellowBag { get; set; }
    public int? BrownBag { get; set; }
    public DateTime? ReceiptDate { get; set; }
    public string UserId { get; set; }

    public int RealEstateId { get; set; }
    public virtual RealEstate RealEstate { get; set; }
}
