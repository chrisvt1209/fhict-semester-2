namespace GameAttic.Application
{
    public interface IGameRepository
    {
        bool AddGame(CreateGameDto game);
        List<GameDto> GetAllGames(GameFilterDto gameFilter);
        GameDto? GetGameById(Guid id);
        bool EditGame(GameDto game);
        bool DeleteGame(Guid id);
        bool AddPlatformToGame(Guid gameId, Guid platformId);
        bool AddGenreToGame(Guid gameId, Guid genreId);
        bool RemovePlatformFromGame(Guid gameId, Guid platformId);
        bool RemoveGenreFromGame(Guid gameId, Guid genreId);
    }
}
