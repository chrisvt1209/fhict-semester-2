using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;

namespace GameAttic.Tests.ServiceTests
{
    public class GenreServiceTests
    {
        private readonly GenreMockRepo _genreMockRepo;
        private readonly IMapper _mapper;
        private readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _genreMockRepo = new GenreMockRepo();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GenreProfile>()).CreateMapper();
            _genreService = new GenreService(_genreMockRepo, _mapper);
        }

        [Fact]
        public void Test_AddGenre_Succeeds()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Genre genre = new Genre(id)
            {
                Id = id,
                Name = "TestGenre 1"
            };
            _genreMockRepo.ReturnValue = true;

            // Act
            bool result = _genreService.AddGenre(genre);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_AddGenre_Fails()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Genre genre = new Genre(id)
            {
                Id = id,
                Name = "TestGenre 2"
            };
            _genreMockRepo.ReturnValue = false;

            // Act
            bool result = _genreService.AddGenre(genre);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_GetAllGenres()
        {
            // Arrange
            _genreMockRepo.MockData =
                new List<GenreDto>
                {
                    new GenreDto()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Genre 1"
                    },

                    new GenreDto()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Genre 2"
                    }
                };

            // Act
            List<Genre> genres = _genreService.GetAllGenres();

            // Assert
            Assert.NotNull(genres);
            Assert.NotNull(genres);
            List<GenreDto> genreDTOs = (List<GenreDto>)_genreMockRepo.MockData;
            Assert.Equal(genreDTOs.Count, genres.Count);

            List<Genre> mappedGenres = _mapper.Map<List<Genre>>(genreDTOs);

            for (int i = 0; i < genreDTOs.Count; i++)
            {
                Assert.Equal(mappedGenres[i].Id, genres[i].Id);
                Assert.Equal(mappedGenres[i].Name, genres[i].Name);
            }
        }

        [Fact]
        public void Test_GetGenreById()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _genreMockRepo.MockData = new GenreDto
            {
                Id = id,
                Name = "Genre 1"
            };

            // Act
            Genre retrievedGenre = _genreService.GetGenreById(id);

            // Assert
            Assert.NotNull(retrievedGenre);
            Genre mappedGenre = _mapper.Map<Genre>((GenreDto)_genreMockRepo.MockData);
            Assert.Equal(mappedGenre.Id, retrievedGenre.Id);
            Assert.Equal(mappedGenre.Name, retrievedGenre.Name);
        }

        [Fact]
        public void Test_GetGenresByGame()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            List<GenreDto> expectedGenreDTOs = new List<GenreDto>
            {
                new GenreDto { Id = Guid.NewGuid(), Name = "Genre 1" },
                new GenreDto { Id = Guid.NewGuid(), Name = "Genre 2" }
            };
            _genreMockRepo.MockData = expectedGenreDTOs;

            // Act
            List<Genre> actualGenres = _genreService.GetGenresByGame(gameId);

            // Assert
            Assert.NotNull(actualGenres);
            Assert.Equal(expectedGenreDTOs.Count, actualGenres.Count);

            List<Genre> mappedGenres = _mapper.Map<List<Genre>>(expectedGenreDTOs);

            for (int i = 0; i < expectedGenreDTOs.Count; i++)
            {
                Assert.Equal(mappedGenres[i].Id, actualGenres[i].Id);
                Assert.Equal(mappedGenres[i].Name, actualGenres[i].Name);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_UpdateGenre(bool expectedResult)
        {
            // Arrange
            Guid genreId = Guid.NewGuid();
            Genre updatedGenre = new Genre(
                genreId,
                "Genre 3"
                );
            _genreMockRepo.ReturnValue = expectedResult;

            // Act
            bool updateResult = _genreService.EditGenre(updatedGenre);

            // Assert
            Assert.Equal(expectedResult, updateResult);
            Genre mappedGenre = _mapper.Map<Genre>((GenreDto)_genreMockRepo.MockData);
            Assert.Equal(mappedGenre.Id, updatedGenre.Id);
            Assert.Equal(mappedGenre.Name, updatedGenre.Name);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_DeleteGenre(bool expectedResult)
        {
            // Arrange
            Guid genreId = Guid.NewGuid();
            _genreMockRepo.ReturnValue = expectedResult;

            // Act
            bool deleteResult = _genreService.DeleteGenre(genreId);

            // Assert
            Assert.Equal(expectedResult, deleteResult);
        }
    }
}
