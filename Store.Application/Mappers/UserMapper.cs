using AutoMapper;
using Store.Application.DataTransferObjects;
using Entity = Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<UserInformationDto, Entity.User>().ReverseMap();
        }
    }
}
