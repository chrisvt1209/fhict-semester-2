using GameAttic.Domain;

namespace GameAttic.Tests.DomainTests
{
    public class GameTests
    {
        [Fact]
        public void GameConstructorHasEqualData()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            string title = "game 1";
            DateOnly releaseDate = new DateOnly();
            decimal price = 4.99m;
            string imageUrl = string.Empty;

            // Act
            Game game = new Game(gameId, title, releaseDate, price, imageUrl);

            // Assert
            Assert.NotNull(game);
            Assert.Equal(gameId, game.Id);
            Assert.Equal(title, game.Title);
            Assert.Equal(releaseDate, game.ReleaseDate);
            Assert.Equal(price, game.Price);
            Assert.Equal(imageUrl, game.ImageUrl);
        }
    }
}
