using BasicRestApi.Services;

namespace BasicRestApi.Tests.Services
{
    public class GameServiceTests
    {

        private readonly GameService _gameService = new();

        [Fact]
        public void GetGameById_WhenGameExists_ReturnMatchingGame()
        {
            // Arrange
            const int gameId = 1;

            // Act
            var result = _gameService.GetGameById(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameId, result.Id);
        }

        [Fact]
        public void GetGameId_WhenGameDoesNotExist_ReturnsNull()
        {
            // Arrange
            const int missingGameId = 999;

            // Act
            var result = _gameService.GetGameById(missingGameId);

            Assert.Null(result);
        }
    }
}
