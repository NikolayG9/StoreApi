using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
