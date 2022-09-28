namespace WasteSegregation.Infrastructure.Data;

public class WasteSegregationDbContext : IdentityDbContext<ApplicationUser>
{
	private readonly UserResolverService userResolverService;

	public WasteSegregationDbContext(DbContextOptions<WasteSegregationDbContext> options,
									 UserResolverService userResolverService) : base(options)
	{
		this.userResolverService = userResolverService;
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
			((AuditableEntity)entityEntry.Entity).LastModifiedBy = userResolverService.GetUser();

			if (entityEntry.State == EntityState.Added)
			{
				((AuditableEntity)entityEntry.Entity).Created = System.DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).CreatedBy = userResolverService.GetUser();
            }
        }
		return await base.SaveChangesAsync();
	}

}
