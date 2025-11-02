using AutoMapper;
using Store.Domain.Entities;

namespace Store.Application.Collections.Dtos
{
    public class CollectionMapper : Profile
    {
        public CollectionMapper()
        {
            CreateMap<CollectionDto, Collection>().ReverseMap();
        }
    }
}
