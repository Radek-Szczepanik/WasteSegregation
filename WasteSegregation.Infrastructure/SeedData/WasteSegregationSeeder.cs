namespace WasteSegregation.Infrastructure.SeedData;

public class WasteSegregationSeeder
{
    private readonly WasteSegregationDbContext dbContext;

    public WasteSegregationSeeder(WasteSegregationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        if (dbContext.Database.CanConnect())
        {
            if (!dbContext.RealEstates.Any())
            {
                var realEstates = GetRealEstates();
                dbContext.RealEstates.AddRange(realEstates);
                dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<RealEstate> GetRealEstates()
    {
        var realEstates = new List<RealEstate>()
        {
            new RealEstate()
            {
                Street = "Polna",
                StreetNumber = "12a",
                PostCode = "21-336",
                City = "Warszawa",
                RealEstateWastes = new List<RealEstateWaste>()
                {
                    new RealEstateWaste()
                    {
                        BlueBag = 1,
                        GreenBag = 1,
                        YellowBag = 3,
                        BrownBag = 2,
                    }
                }
            },
            new RealEstate()
            {
                Street = "Leśna",
                StreetNumber = "21/8",
                PostCode = "05-991",
                City = "Poznań",
                RealEstateWastes = new List<RealEstateWaste>()
                {
                    new RealEstateWaste()
                    {
                        BlueBag = 2,
                        GreenBag = 2,
                        YellowBag = 1,
                        BrownBag = 2,
                    }
                }
            },
            new RealEstate()
            {
                Street = "Zamkowa",
                StreetNumber = "47c",
                PostCode = "12-136",
                City = "Kraków",
                RealEstateWastes = new List<RealEstateWaste>()
                {
                    new RealEstateWaste()
                    {
                        BlueBag = 2,
                        GreenBag = 1,
                        YellowBag = 1,
                        BrownBag = 0,
                    }
                }
            }
        };

        return realEstates;
    }
}
