namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            customerId => customerId.Value,
            dbId => ProductId.Of(dbId));

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
    }
}

