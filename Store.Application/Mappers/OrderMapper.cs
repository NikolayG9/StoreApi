using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<OrderInformation, OrderInformationDto>().ReverseMap();
            CreateMap<ProductOrder, ProductOrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
