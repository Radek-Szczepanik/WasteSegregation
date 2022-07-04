namespace WasteSegregation.DataAccess;

public class WasteSegregationContext : DbContext
{
	public WasteSegregationContext(DbContextOptions<WasteSegregationContext> options) : base(options) { }

	public DbSet<WasteBag> WasteBags { get; set; }
}

