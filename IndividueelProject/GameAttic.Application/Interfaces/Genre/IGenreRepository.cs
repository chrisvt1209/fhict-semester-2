namespace GameAttic.Application
{
    public interface IGenreRepository
    {
        bool AddGenre(GenreDto genre);
        List<GenreDto> GetAllGenres();
        GenreDto? GetGenreById(Guid id);
        public List<GenreDto> GetGenresByGame(Guid gameId);
        bool EditGenre(GenreDto genre);
        bool DeleteGenre(Guid id);
    }
}
