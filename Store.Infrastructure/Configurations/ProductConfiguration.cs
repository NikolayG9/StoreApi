using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(p => p.ProductColors)
                    .WithOne(pc => pc.Product)
                    .HasForeignKey(c => c.ProductId);

            builder.HasMany(p => p.Images)
                   .WithOne()
                   .HasForeignKey(i => i.ProductId);

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ProductType).HasMaxLength(30);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Discount).HasDefaultValue(null);
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
