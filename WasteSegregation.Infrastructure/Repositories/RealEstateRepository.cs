namespace WasteSegregation.Infrastructure.Repositories;

public class RealEstateRepository : IRealEstateRepository
{
    private readonly WasteSegregationDbContext dbContext;

    public RealEstateRepository(WasteSegregationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<RealEstate>> GetAllAsync(int pageNumber, int pageSize, string sortField,
                                                           bool ascending, string filterBy)
        => await dbContext
            .RealEstates
            .Where(s => s.Street.ToLower().Contains(filterBy.ToLower()))
            .Include(x => x.WasteBags)
            .OrderByPropertyName(sortField, ascending)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
         
    public async Task<RealEstate> GetByIdAsync(int id)
        => await dbContext
            .RealEstates
            .Include(x => x.WasteBags)
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

    public async Task<int> GetAllCountAsync(string filterBy)
    {
        return await dbContext.RealEstates.Where(s => s.Street.ToLower().Contains(filterBy.ToLower())).CountAsync();
    }
}
