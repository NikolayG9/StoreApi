using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.CollectionName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.SelectedColor).HasMaxLength(30).IsRequired();
            builder.Property(x => x.SelectedSize).HasMaxLength(30).IsRequired();
            builder.Property(x => x.ProductQuantity).IsRequired();
            builder.Property(x => x.Discount).HasDefaultValue(null);
        }
    }
}
