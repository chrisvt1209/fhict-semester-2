using GameAttic.Domain;

namespace GameAttic.Application
{
    public interface IGenreService
    {
        bool AddGenre(Genre genre);
        List<Genre> GetAllGenres();
        Genre GetGenreById(Guid id);
        public List<Genre> GetGenresByGame(Guid id);
        bool EditGenre(Genre genre);
        bool DeleteGenre(Guid id);
    }
}
