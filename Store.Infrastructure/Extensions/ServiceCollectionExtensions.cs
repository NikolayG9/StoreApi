using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("StoreDb");
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<StoreDbContext>();

            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
