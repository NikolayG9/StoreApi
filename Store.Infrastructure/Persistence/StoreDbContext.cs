using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Configurations;

namespace Store.Infrastructure.Persistence
{
    internal class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        internal DbSet<Collection> Collections { get; set; }
        internal DbSet<Product> Products { get; set; }
        internal DbSet<Color> Colors { get; set; }
        internal DbSet<Image> Images { get; set; }
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<OrderInformation> OrdersInformation { get; set; }
        internal DbSet<ProductOrder> OrderedProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollectionConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColorConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ImageConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderInformation).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductOrderConfiguration).Assembly);
        }
    }
}
