namespace WasteSegregation.DataAccess;

public class WasteSegregationContext : DbContext
{
	public WasteSegregationContext(DbContextOptions<WasteSegregationContext> options) : base(options) { }

	public DbSet<WasteBag> WasteBags { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
	}
}