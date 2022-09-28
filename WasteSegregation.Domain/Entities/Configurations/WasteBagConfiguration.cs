namespace WasteSegregation.Domain.Entities.Configurations;

public class WasteBagConfiguration : IEntityTypeConfiguration<WasteBag>
{
    public void Configure(EntityTypeBuilder<WasteBag> builder)
    {
        builder.Property(x => x.UserId)
               .IsRequired()
               .HasMaxLength(450);
    }
}
