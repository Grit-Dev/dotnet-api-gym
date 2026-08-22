using BasicRestApi.Dtos;
using BasicRestApi.Models;
using BasicRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<List<GameResponse>> GetGames()
        {
            var games = _gameService.GetGames();

            var response = new List<GameResponse>();

            foreach (var game in games)
            {
                response.Add(new GameResponse
                {
                    Id = game.Id,
                    Title = game.Title,
                    Genre = game.Genre,
                    ReleaseYear = game.ReleaseYear,
                    DeveloperId = game.DeveloperId,
                    DeveloperName = game.Developer?.Name ?? string.Empty,
                    Platforms = game.Platforms
                        .Select(platform => platform.Name)
                        .ToList()
                });
            }

            return Ok(response);
        }


        [HttpGet("{id}")]
        public ActionResult<GameResponse> GetGameById(int id)
        {
            var game = _gameService.GetGameById(id);

            if (game is null)
            {
                return NotFound();
            }

            var response = new GameResponse
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                DeveloperId = game.DeveloperId,
                DeveloperName = game.Developer?.Name ?? string.Empty,
                Platforms = game.Platforms
                    .Select(platform => platform.Name)
                    .ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public ActionResult<GameResponse> CreateGame(CreateGameRequest request)
        {
            var platforms = _gameService.GetPlatformsByIds(request.PlatformIds);

            if (platforms.Count != request.PlatformIds.Count)
            {
                return BadRequest("One or more platforms do not exist.");
            }

            var gameAdd = new Game
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear,
                DeveloperId = request.DeveloperId,
                Platforms = platforms.ToList()
            };


            var createdGame = _gameService.CreateGame(gameAdd);

            var response = new GameResponse
            {
                Id = createdGame.Id,
                Title = createdGame.Title,
                Genre = createdGame.Genre,
                ReleaseYear = createdGame.ReleaseYear,
                DeveloperId = createdGame.DeveloperId,
                DeveloperName = createdGame.Developer?.Name ?? string.Empty,
                Platforms = createdGame.Platforms
                    .Select(platform => platform.Name)
                    .ToList()
            };

            return CreatedAtAction(
                nameof(GetGameById),
                new { id = createdGame.Id },
                response);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, UpdateGameRequest request)
        {
            var platforms = _gameService.GetPlatformsByIds(request.PlatformIds);

            if (platforms.Count != request.PlatformIds.Count)
            {
                return BadRequest("One or more platforms do not exist.");
            }

            var updateGame = new Game
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear,
                DeveloperId = request.DeveloperId,
                Platforms = platforms.ToList()
            };

            var gameFound = _gameService.UpdateGame(id, updateGame);

            if (!gameFound)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var gameFound = _gameService.DeleteGame(id);

            if (!gameFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}