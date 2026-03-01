using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Store.Application.Services;
using Store.Application.Services.Interfaces;
using Store.Application.User;
using Store.Application.User.Interfaces;
using Store.Application.Validators;

namespace Store.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddAutoMapper(cfg => { }, applicationAssembly);
            
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

            QuestPDF.Settings.License = LicenseType.Community;

            // Services
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();

            // Validators
            services.AddValidatorsFromAssemblyContaining(typeof(CollectionDtoValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(ProductDtoValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(UserDtoValidator));
        }
    }
}
