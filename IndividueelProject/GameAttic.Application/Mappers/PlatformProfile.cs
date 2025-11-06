using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformDto>();
            CreateMap<PlatformDto, Platform>();
        }
    }
}
