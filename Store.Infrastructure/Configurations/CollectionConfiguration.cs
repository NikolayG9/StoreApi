using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(c => c.Products)
                   .WithOne()
                   .HasForeignKey(p => p.CollectionId);

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
