namespace WasteSegregation.Infrastructure.Data;

public class WasteSegregationDbContext : DbContext
{
	public WasteSegregationDbContext(DbContextOptions<WasteSegregationDbContext> options) : base(options)
	{
	}

	public DbSet<RealEstate> RealEstates { get; set; }
	public DbSet<RealEstateWaste> RealEstateWastes { get; set; }
}
