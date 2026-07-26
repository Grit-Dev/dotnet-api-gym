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

            Assert.Equal(3, retrievedGames.Count);
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
                Title = "Gray Zone Warfare",
                Genre = "Extraction Shooter",
                ReleaseYear = 2022,
            };

            // Act
            var result = _gameService.CreateGame(newGame);

            var retrievedGame = _gameService.GetGameById(result.Id);

            // Assert
            Assert.NotNull(retrievedGame);
            Assert.Equal(result.Id, retrievedGame.Id);
            Assert.Equal(newGame.Title, retrievedGame.Title);
            Assert.Equal(newGame.Genre, retrievedGame.Genre);
            Assert.Equal(newGame.ReleaseYear, retrievedGame.ReleaseYear);
        }

        [Fact]
        public void UpdateGame_WhenCalled_ExistingIdShouldReturnTrue()
        {
            // Arrange
            var updateGame = new Game
            {
                Id = 1,
                Title = "Test",
                Genre = "Test2",
                ReleaseYear = 2026
            };

            // Act
            var actual = _gameService.UpdateGame(updateGame.Id, updateGame);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void UpdateGame_WhenCalled_NonExistingIdShouldReturnFalse()
        {
            // Arrange
            var updateGame = new Game
            {
                Id = 999,
                Title = "Test",
                Genre = "Test2",
                ReleaseYear = 2026
            };

            // Act

            var actual = _gameService.UpdateGame(updateGame.Id, updateGame);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void UpdateGame_WhenCalled_ShouldReturnUpdatedProperties()
        {
            // Arrange
            var newGame = new Game
            {
                Title = "Test",
                Genre = "Test2",
                ReleaseYear = 2000
            };
            
            var gameAdded = _gameService.CreateGame(newGame);

            var updatedGame = new Game
            {
                Title = "Witcher 4",
                Genre = "ACTION RPG",
                ReleaseYear = 2029
            };

            // Act
            var isSuccessful = _gameService.UpdateGame(gameAdded.Id, updatedGame);
            var gameFound = _gameService.GetGameById(gameAdded.Id);

            // Assert
            Assert.True(isSuccessful);
            Assert.NotNull(gameFound);
            Assert.Equal(gameAdded.Id, gameFound.Id);
            Assert.Equal(updatedGame.Title, gameFound.Title);
            Assert.Equal(updatedGame.Genre, gameFound.Genre);
            Assert.Equal(updatedGame.ReleaseYear, gameFound.ReleaseYear);
        }

        [Fact]
        public void DeleteGame_WhenCalled_ShouldDeleteSucessfully()
        {
            // Arrange
            var newGame = new Game
            {
                Title = "Test",
                Genre = "Test",
                ReleaseYear = 1990
            };

            var gameCreated = _gameService.CreateGame(newGame);

            // Act
            var gameDeleted = _gameService.DeleteGame(gameCreated.Id);
            var retrieveGame = _gameService.GetGameById(gameCreated.Id);

            // Assert
            Assert.True(gameDeleted);
            Assert.Null(retrieveGame);
        }

        [Fact]
        public void DeleteGame_WhenCalled_ShouldReturnFalse_WhenGameNotFound()
        {
            // Arrange
            int nonExistentId = int.MaxValue;

            // Act 
            var actual = _gameService.DeleteGame(nonExistentId);

            // Assert
            Assert.False(actual);
        }
    }
}
