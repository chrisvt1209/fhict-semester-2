using GameAttic.Application;

namespace GameAttic.Tests
{
    public class GenreMockRepo : IGenreRepository
    {
        public bool? ReturnValue { get; set; }
        public object MockData { get; internal set; }

        public bool AddGenre(GenreDto genre)
        {
            Assert.NotNull(ReturnValue);
            MockData = genre;

            return ReturnValue.Value;
        }

        public List<GenreDto> GetAllGenres()
        {
            Assert.NotNull(MockData);
            return (List<GenreDto>)MockData;
        }

        public GenreDto? GetGenreById(Guid id)
        {
            Assert.NotNull(MockData);
            return (GenreDto)MockData;
        }

        public List<GenreDto> GetGenresByGame(Guid gameId)
        {
            Assert.NotNull(MockData);
            return (List<GenreDto>)MockData;
        }

        public bool EditGenre(GenreDto genre)
        {
            Assert.NotNull(ReturnValue);
            MockData = genre;

            return ReturnValue.Value;
        }

        public bool DeleteGenre(Guid id)
        {
            Assert.NotNull(ReturnValue);
            return ReturnValue.Value;
        }
    }
}
