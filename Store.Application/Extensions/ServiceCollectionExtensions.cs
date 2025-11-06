using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Services;
using Store.Application.Services.Interfaces;
using Store.Application.Validators;

namespace Store.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddAutoMapper(cfg => { }, applicationAssembly);

            // Services
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IProductService, ProductService>();

            // Validators
            services.AddValidatorsFromAssemblyContaining(typeof(CollectionDtoValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(ProductDtoValidator));
        }
    }
}
