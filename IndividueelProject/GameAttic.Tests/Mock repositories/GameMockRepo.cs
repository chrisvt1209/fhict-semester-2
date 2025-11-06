using GameAttic.Application;

namespace GameAttic.Tests
{
    public class GameMockRepo : IGameRepository
    {
        public bool? ReturnValue { get; set; }
        public object MockData { get; internal set; }

        public bool AddGame(CreateGameDto game)
        {
            Assert.NotNull(ReturnValue);
            MockData = game;

            return ReturnValue.Value;
        }

        public List<GameDto> GetAllGames(GameFilterDto gameFilterDto)
        {
            Assert.NotNull(MockData);
            return (List<GameDto>)MockData;
        }

        public GameDto? GetGameById(Guid id)
        {
            Assert.NotNull(MockData);
            return (GameDto)MockData;
        }

        public bool EditGame(GameDto game)
        {
            Assert.NotNull(ReturnValue);
            MockData = game;

            return ReturnValue.Value;
        }

        public bool DeleteGame(Guid id)
        {
            Assert.NotNull(ReturnValue);
            return ReturnValue.Value;
        }

        public bool AddPlatformToGame(Guid gameId, Guid platformId)
        {
            Assert.NotNull(ReturnValue);
            MockData = new { GameId = gameId, PlatformId = platformId };

            return ReturnValue.Value;
        }

        public bool AddGenreToGame(Guid gameId, Guid genreId)
        {
            Assert.NotNull(ReturnValue);
            MockData = new { GameId = gameId, GenreId = genreId };

            return ReturnValue.Value;
        }

        public bool RemovePlatformFromGame(Guid gameId, Guid platformId)
        {
            Assert.NotNull(ReturnValue);
            MockData = new { GameId = gameId, PlatformId = platformId };

            return ReturnValue.Value;
        }

        public bool RemoveGenreFromGame(Guid gameId, Guid genreId)
        {
            Assert.NotNull(ReturnValue);
            MockData = new { GameId = gameId, GenreId = genreId };

            return ReturnValue.Value;
        }
    }
}