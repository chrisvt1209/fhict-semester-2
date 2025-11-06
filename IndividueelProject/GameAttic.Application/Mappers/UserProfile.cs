using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, RegistrationUserDto>();
            CreateMap<RegistrationUserDto, User>();
        }
    }
}
