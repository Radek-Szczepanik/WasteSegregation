namespace WasteSegregation.Infrastructure.Data;

public class WasteSegregationDbContext : DbContext
{
	public WasteSegregationDbContext(DbContextOptions<WasteSegregationDbContext> options) : base(options)
	{
	}

	public DbSet<RealEstate> RealEstates { get; set; }
	public DbSet<WasteBag> WasteBags { get; set; }

	public async Task<int> SaveChangesAsync()
	{
		var entries = ChangeTracker
			.Entries()
			.Where(e => e.Entity is AuditableEntity && 
			      (e.State == EntityState.Added || e.State == EntityState.Modified));

		foreach (var entityEntry in entries)
		{
			((AuditableEntity)entityEntry.Entity).LastModified = System.DateTime.UtcNow;
			//((AuditableEntity)entityEntry.Entity).LastModifiedBy = 

			if (entityEntry.State == EntityState.Added)
			{
				((AuditableEntity)entityEntry.Entity).Created = System.DateTime.UtcNow;
                //((AuditableEntity)entityEntry.Entity).LastModifiedBy = 
            }
        }
		return await base.SaveChangesAsync();
	}

}
