namespace WasteSegregation.Domain.Entities.Configurations;

public class RealEstateConfiguration : IEntityTypeConfiguration<RealEstate>
{
    public void Configure(EntityTypeBuilder<RealEstate> builder)
    {
        builder.Property(x => x.Street)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(x => x.StreetNumber)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(x => x.PostCode)
               .IsRequired()
               .HasMaxLength(6);

        builder.Property(x => x.City)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasMany(x => x.RealEstateWastes)
               .WithOne(y => y.RealEstate)
               .HasForeignKey(z => z.RealEstateId);
    }
}
