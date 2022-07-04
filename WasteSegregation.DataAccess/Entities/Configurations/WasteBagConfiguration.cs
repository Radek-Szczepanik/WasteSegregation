namespace WasteSegregation.DataAccess.Entities.Configurations;

public class WasteBagConfiguration : IEntityTypeConfiguration<WasteBag>
{
    public void Configure(EntityTypeBuilder<WasteBag> builder)
    {
        builder.Property(x => x.YellowBag)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.GreenBag)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BlueBag)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BrownBag)
            .IsRequired()
            .HasMaxLength(50);
    }
}