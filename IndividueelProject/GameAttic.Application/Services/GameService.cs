using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public bool AddGame(Game game, Guid platformId, Guid genreId)
        {
            CreateGameDto gameDTO = ConvertGame.ToCreateDTO(game, platformId, genreId);
            bool addedGame = _gameRepository.AddGame(gameDTO);
            return addedGame;
        }

        public List<Game> GetAllGames(GameFilter gameFilter)
        {
            GameFilterDto gameFilterDto = _mapper.Map<GameFilterDto>(gameFilter);
            List<GameDto> gameDTOs = _gameRepository.GetAllGames(gameFilterDto);
            List<Game> games = new List<Game>();
            foreach (GameDto gameDTO in gameDTOs)
            {
                games.Add(_mapper.Map<Game>(gameDTO));
            }
            return games;
        }

        public Game GetGameById(Guid id)
        {
            GameDto gameDTO = _gameRepository.GetGameById(id)!;
            Game game = _mapper.Map<Game>(gameDTO);
            return game;
        }

        public bool EditGame(Game game)
        {
            GameDto gameDTO = _mapper.Map<GameDto>(game);
            bool updatedGame = _gameRepository.EditGame(gameDTO);
            return updatedGame;
        }

        public bool DeleteGame(Guid id)
        {
            bool deletedGame = _gameRepository.DeleteGame(id);
            return deletedGame;
        }

        public bool AddPlatformToGame(Guid gameId, Guid platformId)
        {
            bool addedPlatform = _gameRepository.AddPlatformToGame(gameId, platformId);
            return addedPlatform;
        }

        public bool AddGenreToGame(Guid gameId, Guid genreId)
        {
            bool addedGenre = _gameRepository.AddGenreToGame(gameId, genreId);
            return addedGenre;
        }

        public bool RemovePlatformFromGame(Guid gameId, Guid platformId)
        {
            bool removedPlatform = false;
            if (platformId != Guid.Empty)
            {
                removedPlatform = _gameRepository.RemovePlatformFromGame(gameId, platformId);
            }
            return removedPlatform;
        }

        public bool RemoveGenreFromGame(Guid gameId, Guid genreId)
        {
            bool removedGenre = false;
            if (genreId != Guid.Empty)
            {
                removedGenre = _gameRepository.RemoveGenreFromGame(gameId, genreId);
            }
            return removedGenre;
        }
    }
}