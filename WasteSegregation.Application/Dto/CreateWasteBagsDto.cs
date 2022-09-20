namespace WasteSegregation.Application.Dto;

public class CreateWasteBagsDto
{
    public byte? BlueBag { get; set; }
    public byte? GreenBag { get; set; }
    public byte? YellowBag { get; set; }
    public byte? BrownBag { get; set; }
    public DateTime? ReceiptDate { get; set; }
}
