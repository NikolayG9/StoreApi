using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class ColorMapper : Profile
    {
        public ColorMapper()
        {
            CreateMap<ColorDto, Color>().ReverseMap();
        }
    }
}
