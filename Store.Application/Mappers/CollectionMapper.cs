using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class CollectionMapper : Profile
    {
        public CollectionMapper()
        {
            CreateMap<CollectionDto, Collection>().ReverseMap();
        }
    }
}
