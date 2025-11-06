using GameAttic.Domain;

namespace GameAttic.Tests.DomainTests
{
    public class GameFilterTests
    {
        [Fact]
        public void GameFilterConstructorHasEqualData()
        {
            // Arrange
            string? title = null;
            Guid? platformId = null;
            Guid? genreId = null;
            GameOrderColumn orderColumn = GameOrderColumn.Title;
            bool isAsc = true;

            // Act
            GameFilter gameFilter = new GameFilter();

            // Assert
            Assert.Equal(title, gameFilter.Title);
            Assert.Equal(platformId, gameFilter.PlatformId);
            Assert.Equal(genreId, gameFilter.GenreId);
            Assert.Equal(orderColumn, gameFilter.OrderColumn);
            Assert.Equal(isAsc, gameFilter.IsAsc);
        }
    }
}
