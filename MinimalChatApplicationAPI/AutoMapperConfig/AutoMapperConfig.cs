using AutoMapper;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.AutoMapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<User, ResUserLoginDto>().ReverseMap();
            CreateMap<User, ResUserRegistrationDto>().ReverseMap();
        }
    }
}
