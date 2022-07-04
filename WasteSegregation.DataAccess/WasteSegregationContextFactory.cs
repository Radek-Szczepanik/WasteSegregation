namespace WasteSegregation.DataAccess
{
    public class WasteSegregationContextFactory : IDesignTimeDbContextFactory<WasteSegregationContext>
    {
        public WasteSegregationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WasteSegregationContext>();
            optionsBuilder.UseSqlServer("Data Source =.\\SQLEXPRESS; Initial Catalog = WasteSegregationDb; Integrated Security = True");
            return new WasteSegregationContext(optionsBuilder.Options);
        }
    }
}