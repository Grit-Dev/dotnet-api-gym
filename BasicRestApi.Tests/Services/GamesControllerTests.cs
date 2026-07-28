using BasicRestApi.Controllers;
using BasicRestApi.Dtos;
using BasicRestApi.Models;
using BasicRestApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasicRestApi.Tests.Services
{
    public class GamesControllerTests
    {
        private readonly Mock<IGameService> _gameServiceMock;

        private readonly GamesController _controller;

        public GamesControllerTests()
        {
            _gameServiceMock = new Mock<IGameService>();

            // .Object is the pretend IGameService implementation that can be passed into the controller constructor.
            _controller = new GamesController(_gameServiceMock.Object);

        }

        [Fact]
        public void GetGameById_WhenGamesDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            const int missingGameId = 901;

            _gameServiceMock
                .Setup(Service => Service.GetGameById(missingGameId))
                .Returns((Game?)null);

            // Act
            var result = _controller.GetGameById(missingGameId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);

            _gameServiceMock.Verify(
                service => service.GetGameById(missingGameId),Times.Once);
        }

        [Fact]
        public void GetGameById_WhenGameExists_ReturnOkWithGameResponse()
        {
            // arrange 
            const int gameId = 1;

            var game = new Game
            {
                Id = gameId,
                Title = "Cyberpunk 2077",
                Genre = "Action RPG",
                ReleaseYear = 2020
            };

            _gameServiceMock
                .Setup(service => service.GetGameById(gameId))
                .Returns(game);

            // Act

            // Call the real controller method.
            // Returns an ActionResult<GameResponse>, which is the outer wrapper
            // containing the controller's response.
            var result = _controller.GetGameById(gameId);

            // Assert
            // Get the action result stored inside the wrapper.
            // Because the controller returned Ok(response), this should be an
            // OkObjectResult, representing 200 OK.
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Get the response body stored inside the 200 OK result.
            // Because the controller passed a GameResponse into Ok(response),
            // okResult.Value should contain a GameResponse.
            var response = Assert.IsType<GameResponse>(okResult.Value);

            Assert.Equal(game.Id, response.Id);
            Assert.Equal(game.Title, response.Title);
            Assert.Equal(game.Genre, response.Genre);
            Assert.Equal(game.ReleaseYear, response.ReleaseYear);

            _gameServiceMock.Verify(
                service => service.GetGameById(gameId), Times.Once);

        }

    }
}
