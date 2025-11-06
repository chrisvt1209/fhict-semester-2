using GameAttic.Domain;

namespace GameAttic.Application
{
    public interface IGameService
    {
        bool AddGame(Game game, Guid platformId, Guid genreId);
        List<Game> GetAllGames(GameFilter gameFilter);
        Game GetGameById(Guid id);
        bool EditGame(Game game);
        bool DeleteGame(Guid id);
        bool AddPlatformToGame(Guid gameId, Guid platformId);
        bool AddGenreToGame(Guid gameId, Guid genreId);
        bool RemovePlatformFromGame(Guid gameId, Guid platformId);
        bool RemoveGenreFromGame(Guid gameId, Guid genreId);
    }
}
