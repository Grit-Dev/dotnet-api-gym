using BasicRestApi.Models;
using BasicRestApi.Services;

namespace BasicRestApi.Tests.Services
{
    public class GameServiceTests
    {

        private readonly GameService _gameService = new();

        [Theory]
        [InlineData(1, "Witcher 3", "Action RPG", 2020)]
        [InlineData(2, "Cyberpunk 2077", "Action RPG", 2020)]
        [InlineData(3, "Crimson Desert", "Action RPG", 2020)]
        public void GetAllGames_WhenCalled_ShouldReturnAllSeededGames(
            int expectedId, 
            string expectedTitle, 
            string expectedGenre,
            int expectedReleaseYear)
        {
            // Act
            var retrievedGames = _gameService.GetGames();

            // Assert
            var game = Assert.Single(
                retrievedGames,
                game => game.Id == expectedId);

            Assert.Equal(expectedTitle, game.Title);
            Assert.Equal(expectedGenre, game.Genre);
            Assert.Equal(expectedReleaseYear, game.ReleaseYear);
        }

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

        [Fact]
        public void CreateGame_ReturnMatchGame()
        {
            // Arrange
            var newGame = new Game
            {
                Title = "Metal Gear Solid",
                Genre = "Tactical Espionage",
                ReleaseYear = 1993
            };

            // Act
            var result = _gameService.CreateGame(newGame);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(newGame.Title, result.Title);
            Assert.Equal(newGame.Genre, result.Genre);
            Assert.Equal(newGame.ReleaseYear, result.ReleaseYear);
        }

        [Fact]
        public void CreateGame_WhenCalled_ShouldBeStoresSuccessfully()
        {
            // Arrange
            var newGame = new Game
            {
                Title = "Gray Zone Warefare",
                Genre = "Extraction Shooter",
                ReleaseYear = 2022,
            };

            // Act
            var result = _gameService.CreateGame(newGame);

            var retrievedGame = _gameService.GetGameById(result.Id);

            // Assert
            Assert.NotNull(retrievedGame);
            Assert.Equal(result.Id, retrievedGame.Id);
            Assert.Equal(retrievedGame.Title, newGame.Title);
            Assert.Equal(retrievedGame.Genre, newGame.Genre);
            Assert.Equal(retrievedGame.ReleaseYear, newGame.ReleaseYear);
        }
    }
}
