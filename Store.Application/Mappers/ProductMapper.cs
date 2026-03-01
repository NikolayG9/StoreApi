using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.Colors,
                    opt => opt.MapFrom(src => src.ProductColors.Select(pc => pc.Color))
                );

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductColors, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
