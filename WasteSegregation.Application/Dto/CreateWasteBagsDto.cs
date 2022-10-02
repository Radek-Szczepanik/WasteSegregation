namespace WasteSegregation.Application.Dto;

public class CreateWasteBagsDto
{
    public int? BlueBag { get; set; }
    public int? GreenBag { get; set; }
    public int? YellowBag { get; set; }
    public int? BrownBag { get; set; }
    public DateTime? ReceiptDate { get; set; }
}
