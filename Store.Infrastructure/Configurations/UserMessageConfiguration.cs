using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations
{
    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.UserEmail).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Message).IsRequired();
        }
    }
}
