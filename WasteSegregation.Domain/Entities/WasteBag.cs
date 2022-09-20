namespace WasteSegregation.Domain.Entities;

public class WasteBag
{
    public int Id { get; set; }
    public byte? BlueBag { get; set; }
    public byte? GreenBag { get; set; }
    public byte? YellowBag { get; set; }
    public byte? BrownBag { get; set; }
    public DateTime? ReceiptDate { get; set; }

    public int RealEstateId { get; set; }
    public virtual RealEstate RealEstate { get; set; }
}
