using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Collections;
using Store.Application.Collections.Dtos;
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

            // Validators
            services.AddValidatorsFromAssemblyContaining(typeof(CollectionDtoValidator));
        }
    }
}
