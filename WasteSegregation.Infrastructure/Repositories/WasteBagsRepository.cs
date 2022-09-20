namespace WasteSegregation.Infrastructure.Repositories;

public class WasteBagsRepository : IWasteBagsRepository
{
    private readonly WasteSegregationDbContext dbContext;

    public WasteBagsRepository(WasteSegregationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<WasteBag> AddAsync(WasteBag wasteBags)
    {
        var createWasteBags = await dbContext.WasteBags.AddAsync(wasteBags);
        await dbContext.SaveChangesAsync();
        return createWasteBags.Entity;
    }
}
