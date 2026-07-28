using BasicRestApi.Controllers;
using BasicRestApi.Models;
using BasicRestApi.Services;
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
            var result = _controller?.GetGameById(missingGameId);

            // Assert
            Assert.IsType<NotFoundResult>(result?.Result);

            _gameServiceMock.Verify(
                service => service.GetGameById(missingGameId),Times.Once);
        }
    }
}
