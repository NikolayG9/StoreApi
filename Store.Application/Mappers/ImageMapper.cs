using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class ImageMapper : Profile
    {
        public ImageMapper()
        {
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<ImageFileDto, Image>().ReverseMap();
        }
    }
}
