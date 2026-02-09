using AutoMapper;
using Store.Application.DataTransferObjects;
using Store.Domain.Entities;

namespace Store.Application.Mappers
{
    public class UserMessageMapper : Profile
    {
        public UserMessageMapper()
        {
            CreateMap<UserMessage, UserMessageDto>().ReverseMap();
        }
    }
}
