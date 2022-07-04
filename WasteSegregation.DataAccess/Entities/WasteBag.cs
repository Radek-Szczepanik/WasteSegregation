namespace WasteSegregation.DataAccess.Entities;

public class WasteBag : EntityBase
{
    public int YellowBag { get; set; }
    public int GreenBag { get; set; }
    public int BlueBag { get; set; }
    public int BrownBag { get; set; }
}