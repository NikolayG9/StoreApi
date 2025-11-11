using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Store.Application.DataTransferObjects;

namespace Store.Application.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleDto, IdentityRole>().ReverseMap();
        }
    }
}
