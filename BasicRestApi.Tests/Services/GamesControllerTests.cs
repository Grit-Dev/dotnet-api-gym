using BasicRestApi.Controllers;
using BasicRestApi.Dtos;
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

        private readonly List<Platform> _platforms =
        [
            new Platform
            {
                Id = 1,
                Name = "PlayStation 5",
                Manufacturer = "Sony",
                ReleaseYear = 2020
            },
            new Platform
            {
                Id = 2,
                Name = "Xbox Series X",
                Manufacturer = "Microsoft",
                ReleaseYear = 2020
            }
        ];


        public GamesControllerTests()
        {
            _gameServiceMock = new Mock<IGameService>();

            _controller = new GamesController(_gameServiceMock.Object);
        }

        [Fact]
        public void GetGameById_WhenGamesDoesNotExist_ReturnsNotFound()
        {
            const int missingGameId = 901;

            _gameServiceMock
                .Setup(service => service.GetGameById(missingGameId))
                .Returns((Game?)null);

            var result = _controller.GetGameById(missingGameId);

            Assert.IsType<NotFoundResult>(result.Result);

            _gameServiceMock.Verify(
                service => service.GetGameById(missingGameId),
                Times.Once);
        }

        [Fact]
        public void GetGameById_WhenGameExists_ReturnOkWithGameResponse()
        {
            const int gameId = 1;


            var game = new Game
            {
                Id = gameId,
                Title = "Cyberpunk 2077",
                Genre = "Action RPG",
                ReleaseYear = 2020,
                DeveloperId = 1,
                Developer = new Developer
                {
                    Id = 1,
                    Name = "CD PROJEKT"
                },
                Platforms = _platforms
            };


            _gameServiceMock
                .Setup(service => service.GetGameById(gameId))
                .Returns(game);

            var result = _controller.GetGameById(gameId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var response = Assert.IsType<GameResponse>(okResult.Value);

            Assert.Equal(game.Id, response.Id);
            Assert.Equal(game.Title, response.Title);
            Assert.Equal(game.Genre, response.Genre);
            Assert.Equal(game.ReleaseYear, response.ReleaseYear);
            Assert.Equal(game.DeveloperId, response.DeveloperId);
            Assert.Equal(game.Developer.Name, response.DeveloperName);

            Assert.Equal(2, response.Platforms.Count);


            _gameServiceMock.Verify(
                service => service.GetGameById(gameId),
                Times.Once);
        }


        [Fact]
        public void GetGames_WhenGamesExist_ReturnsOkWithGameResponses()
        {
            var games = new List<Game>
            {
                new()
                {
                    Id = 1,
                    Title = "Cyberpunk 2077",
                    Genre = "Action RPG",
                    ReleaseYear = 2020,
                    DeveloperId = 1,
                    Developer = new Developer
                    {
                        Id = 1,
                        Name = "CD PROJEKT"
                    },
                    Platforms = _platforms
                },
                new()
                {
                    Id = 2,
                    Title = "The Witcher 3",
                    Genre = "Action RPG",
                    ReleaseYear = 2015,
                    DeveloperId = 1,
                    Developer = new Developer
                    {
                        Id = 1,
                        Name = "CD PROJEKT"
                    },
                    Platforms = _platforms
                }
            };

            _gameServiceMock
                .Setup(service => service.GetGames())
                .Returns(games);

            var result = _controller.GetGames();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var response = Assert.IsType<List<GameResponse>>(okResult.Value);


            Assert.Equal(2, response.Count);


            Assert.Equal(
                games[0].Platforms.Count,
                response[0].Platforms.Count);

            _gameServiceMock.Verify(
                service => service.GetGames(),
                Times.Once);
        }

        [Fact]
        public void DeleteGame_WhenGameExists_ReturnsNoContent()
        {
            const int gameId = 1;

            _gameServiceMock
                .Setup(service => service.DeleteGame(gameId))
                .Returns(true);

            var result = _controller.DeleteGame(gameId);

            Assert.IsType<NoContentResult>(result);

            _gameServiceMock.Verify(
                service => service.DeleteGame(gameId),
                Times.Once);
        }

        [Fact]
        public void DeleteGame_WhenGameDoesNotExist_ReturnsNotFound()
        {
            const int gameId = 999;

            _gameServiceMock
                .Setup(service => service.DeleteGame(gameId))
                .Returns(false);

            var result = _controller.DeleteGame(gameId);

            Assert.IsType<NotFoundResult>(result);

            _gameServiceMock.Verify(
                service => service.DeleteGame(gameId),
                Times.Once);
        }

        [Fact]
        public void UpdateGame_WhenGameExists_ReturnsNoContent()
        {
            const int gameId = 1;

            var request = new UpdateGameRequest
            {
                Title = "Cyberpunk Updated",
                Genre = "Action RPG",
                ReleaseYear = 2021,
                DeveloperId = 1,
                PlatformIds = new List<int>
                {
                    2
                }
            };

            _gameServiceMock
                .Setup(service =>
                    service.GetPlatformsByIds(request.PlatformIds))
                .Returns(_platforms
                    .Where(p => request.PlatformIds.Contains(p.Id))
                    .ToList());

            _gameServiceMock
                .Setup(service =>
                    service.UpdateGame(
                        gameId,
                        It.IsAny<Game>()))
                .Returns(true);

            var result = _controller.UpdateGame(gameId, request);

            Assert.IsType<NoContentResult>(result);

            _gameServiceMock.Verify(
                service =>
                    service.UpdateGame(
                        gameId,
                        It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public void UpdateGame_WhenGameDoesNotExist_ReturnsNotFound()
        {
            const int gameId = 999;

            var request = new UpdateGameRequest
            {
                Title = "Missing Game",
                Genre = "Unknown",
                ReleaseYear = 2025,
                DeveloperId = 1,
                PlatformIds = new List<int>
                {
                    1
                }
            };

            _gameServiceMock
                .Setup(service =>
                    service.GetPlatformsByIds(request.PlatformIds))
                .Returns(
                    _platforms
                        .Where(p => request.PlatformIds.Contains(p.Id))
                        .ToList());

            _gameServiceMock
                .Setup(service =>
                    service.UpdateGame(
                        gameId,
                        It.IsAny<Game>()))
                .Returns(false);

            var result = _controller.UpdateGame(gameId, request);

            Assert.IsType<NotFoundResult>(result);

            _gameServiceMock.Verify(
                service =>
                    service.UpdateGame(
                        gameId,
                        It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public void CreateGame_WhenRequestIsValid_ReturnsCreatedAtActionWithGameResponse()
        {
            var request = new CreateGameRequest
            {
                Title = "Elden Ring",
                Genre = "Action RPG",
                ReleaseYear = 2022,
                DeveloperId = 1,
                PlatformIds = new List<int>
                {
                    1
                }
            };

            _gameServiceMock
                .Setup(service =>
                    service.GetPlatformsByIds(request.PlatformIds))
                .Returns(
                    _platforms
                        .Where(p => request.PlatformIds.Contains(p.Id))
                        .ToList());

            var createdGame = new Game
            {
                Id = 3,
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear,
                DeveloperId = request.DeveloperId,

                Developer = new Developer
                {
                    Id = 1,
                    Name = "CD PROJEKT"
                },

                Platforms = _platforms
                    .Where(p => request.PlatformIds.Contains(p.Id))
                    .ToList()
            };

            _gameServiceMock
                .Setup(service =>
                    service.CreateGame(It.IsAny<Game>()))
                .Returns(createdGame);

            var result = _controller.CreateGame(request);

            var createdResult =
                Assert.IsType<CreatedAtActionResult>(result.Result);

            var response =
                Assert.IsType<GameResponse>(createdResult.Value);

            Assert.Equal(createdGame.Id, response.Id);
            Assert.Equal(createdGame.Title, response.Title);
            Assert.Equal(createdGame.DeveloperId, response.DeveloperId);

            Assert.Single(response.Platforms);
            Assert.Contains("PlayStation 5", response.Platforms);

            _gameServiceMock.Verify(
                service =>
                    service.CreateGame(It.IsAny<Game>()),
                Times.Once);

            Assert.Equal(
                nameof(GamesController.GetGameById),
                createdResult.ActionName);
        }
    }
}