using GameAttic.Domain;

namespace GameAttic.Tests.DomainTests
{
    public class GenreTests
    {
        [Fact]
        public void GenreConstructorHasSameData()
        {
            Guid genreId = Guid.NewGuid();
            string name = "Genre 1";

            Genre genre = new Genre(genreId, name);

            Assert.Equal(genreId, genre.Id);
            Assert.Equal(name, genre.Name);
        }
    }
}
