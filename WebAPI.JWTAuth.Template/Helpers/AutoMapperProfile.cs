using AutoMapper;
using WebAPI.JWTAuth.Template.Dto;
using WebAPI.JWTAuth.Template.Models;

namespace WebAPI.JWTAuth.Template.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
