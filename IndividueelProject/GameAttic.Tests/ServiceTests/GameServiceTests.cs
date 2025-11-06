using AutoMapper;
using GameAttic.Application;
using GameAttic.Domain;

namespace GameAttic.Tests.ServiceTests
{
    public class GameServiceTests
    {
        private readonly IGameService _gameService;
        private readonly GameMockRepo _gameMockRepo;
        private readonly IMapper _mapper;


        public GameServiceTests()
        {
            _gameMockRepo = new GameMockRepo();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new GameProfile())).CreateMapper();
            _gameService = new GameService(_gameMockRepo, _mapper);
        }

        [Fact]
        public void Test_AddGames_Succeeds()
        {
            // Arrange            
            Guid id = Guid.NewGuid();
            Guid platformId = Guid.NewGuid();
            Guid genreId = Guid.NewGuid();
            Game game = new Game(id)
            {
                Title = "Testgame 1",
                ReleaseDate = new DateOnly(),
                Price = 4.99m,
                ImageUrl = ""

            };
            _gameMockRepo.ReturnValue = true;

            // Act
            bool result = _gameService.AddGame(game, platformId, genreId);

            // Assert
            Assert.True(result);
            Assert.True(CreateGameDTOAndGameAreEqual(game, (CreateGameDto)_gameMockRepo.MockData));
        }

        [Fact]
        public void Test_AddGames_Fails()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid platformId = Guid.NewGuid();
            Guid genreId = Guid.NewGuid();
            Game game = new Game(gameId)
            {
                Title = "Testgame 2",
                ReleaseDate = new DateOnly(),
                Price = 4.99m,
                ImageUrl = ""

            };
            _gameMockRepo.ReturnValue = false;

            // Act
            bool result = _gameService.AddGame(game, platformId, genreId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_GetAllGames()
        {
            // Arrange
            GameFilter filter = new GameFilter();
            _gameMockRepo.MockData =
                new List<GameDto>
                {
                    new GameDto()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Game1",
                        ReleaseDate = new DateOnly(),
                        Price = 4.99m
                    },

                    new GameDto()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Game2",
                        ReleaseDate = new DateOnly(),
                        Price = 14.99m,
                        ImageUrl = "https://www.example.com/image.jpg"
                    }

                };

            // Act
            List<Game> games = _gameService.GetAllGames(filter);

            // Assert
            Assert.NotNull(games);
            List<GameDto> gameDTOs = (List<GameDto>)_gameMockRepo.MockData;
            Assert.Equal(gameDTOs.Count, games.Count);

            List<Game> mappedGames = _mapper.Map<List<Game>>(gameDTOs);

            for (int i = 0; i < gameDTOs.Count; i++)
            {
                Assert.Equal(mappedGames[i].Id, games[i].Id);
                Assert.Equal(mappedGames[i].Title, games[i].Title);
                Assert.Equal(mappedGames[i].ReleaseDate, games[i].ReleaseDate);
                Assert.Equal(mappedGames[i].Price, games[i].Price);
                Assert.Equal(mappedGames[i].ImageUrl, games[i].ImageUrl);
            }
        }

        [Fact]
        public void Test_GetGameById()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _gameMockRepo.MockData = new GameDto
            {
                Id = id,
                Title = "Game1",
                ReleaseDate = new DateOnly(),
                Price = 4.99m
            };

            // Act
            Game retrievedGame = _gameService.GetGameById(id);

            // Assert
            Assert.NotNull(retrievedGame);
            Game mappedGame = _mapper.Map<Game>((GameDto)_gameMockRepo.MockData);
            Assert.Equal(mappedGame.Id, retrievedGame.Id);
            Assert.Equal(mappedGame.Title, retrievedGame.Title);
            Assert.Equal(mappedGame.ReleaseDate, retrievedGame.ReleaseDate);
            Assert.Equal(mappedGame.Price, retrievedGame.Price);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_UpdateGame(bool expectedResult)
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game updatedGame = new Game(
                gameId,
                "Game 3",
                new DateOnly(),
                2.59m,
                ""
                );
            _gameMockRepo.ReturnValue = expectedResult;

            // Act
            bool updateResult = _gameService.EditGame(updatedGame);

            // Assert
            Assert.Equal(expectedResult, updateResult);
            Game mappedGame = _mapper.Map<Game>((GameDto)_gameMockRepo.MockData);
            Assert.Equal(mappedGame.Id, updatedGame.Id);
            Assert.Equal(mappedGame.Title, updatedGame.Title);
            Assert.Equal(mappedGame.ReleaseDate, updatedGame.ReleaseDate);
            Assert.Equal(mappedGame.Price, updatedGame.Price);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_DeleteGame(bool expectedResult)
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            _gameMockRepo.ReturnValue = expectedResult;

            // Act
            bool deleteResult = _gameService.DeleteGame(gameId);

            // Assert
            Assert.Equal(expectedResult, deleteResult);
        }

        [Fact]
        public void Test_AddPlatformToGame_Succeeds()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid platformId = Guid.NewGuid();
            _gameMockRepo.ReturnValue = true;

            // Act
            bool result = _gameService.AddPlatformToGame(gameId, platformId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_RemovePlatformFromGame()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid platformId = Guid.NewGuid();
            _gameMockRepo.ReturnValue = true;

            // Act
            bool result = _gameService.RemovePlatformFromGame(gameId, platformId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_AddGenreToGame_Succeeds()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid genreId = Guid.NewGuid();
            _gameMockRepo.ReturnValue = true;

            // Act
            bool result = _gameService.AddGenreToGame(gameId, genreId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_RemovGenreFromGame()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid genreId = Guid.NewGuid();
            _gameMockRepo.ReturnValue = true;

            // Act
            bool result = _gameService.RemoveGenreFromGame(gameId, genreId);

            // Assert
            Assert.True(result);
        }

        private bool CreateGameDTOAndGameAreEqual(Game game, CreateGameDto createGameDTO)
        {
            return
                game.Title == createGameDTO.Title &&
                game.ReleaseDate == createGameDTO.ReleaseDate &&
                game.Price == createGameDTO.Price &&
                game.ImageUrl == createGameDTO.ImageUrl
                ;
        }
    }
}
