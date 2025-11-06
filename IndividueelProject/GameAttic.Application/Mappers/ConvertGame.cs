using GameAttic.Domain;

namespace GameAttic.Application
{
    public static class ConvertGame
    {
        public static CreateGameDto ToCreateDTO(Game game, Guid platformId, Guid genreId)
        {
            CreateGameDto gameDTO = new CreateGameDto()
            {
                GameId = game.Id,
                PlatformId = platformId,
                Title = game.Title,
                ReleaseDate = game.ReleaseDate,
                GenreId = genreId,
                Price = game.Price,
                ImageUrl = game.ImageUrl
            };
            return gameDTO;
        }
    }
}
