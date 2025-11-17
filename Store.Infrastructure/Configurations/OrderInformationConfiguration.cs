    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class OrderInformationConfiguration : IEntityTypeConfiguration<OrderInformation>
    {
        public void Configure(EntityTypeBuilder<OrderInformation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(80).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(40).IsRequired();
            builder.Property(x => x.City).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.OrderDetails).HasMaxLength(500);
        }
    }
}
