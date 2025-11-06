using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, CreateGameDto>();
            CreateMap<GameFilter, GameFilterDto>().ReverseMap();
        }
    }
}
