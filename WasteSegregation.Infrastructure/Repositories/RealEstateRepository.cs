namespace WasteSegregation.Infrastructure.Repositories;

public class RealEstateRepository : IRealEstateRepository
{
    private readonly WasteSegregationDbContext dbContext;

    public RealEstateRepository(WasteSegregationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<RealEstate>> GetAllAsync()
        => await dbContext
            .RealEstates
            .Include(x => x.RealEstateWastes)
            .ToListAsync();
         
    public async Task<RealEstate> GetByIdAsync(int id)
        => await dbContext
            .RealEstates
            .Include(x => x.RealEstateWastes)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<RealEstate> AddAsync(RealEstate realEstate)
    {
        var createdRealEstate = await dbContext.RealEstates.AddAsync(realEstate);
        await dbContext.SaveChangesAsync();
        return createdRealEstate.Entity;
    }

    public async Task UpdateAsync(RealEstate realEstate)
    {
        dbContext.RealEstates.Update(realEstate);
        await dbContext.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(RealEstate realEstate)
    {
        dbContext.RealEstates.Remove(realEstate);
        await dbContext.SaveChangesAsync();
        await Task.CompletedTask;
    }
}
