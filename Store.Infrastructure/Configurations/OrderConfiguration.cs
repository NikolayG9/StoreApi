using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasMany(o => o.OrderedProducts)
                   .WithOne()
                   .HasForeignKey(p => p.ProductId);
            builder.HasOne(o => o.OrderInformation)
                   .WithOne()
                   .HasForeignKey<OrderInformation>(i => i.ProductId);

            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.TotalDiscount).HasDefaultValue(null);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.IsSoftDeleted).HasDefaultValue(false);
        }
    }
}
