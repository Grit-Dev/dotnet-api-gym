using BasicRestApi.Dtos;
using BasicRestApi.Models;
using BasicRestApi.Services;
using Microsoft.AspNetCore.Mvc;

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
        // Returning data? Prefer ActionResult<T>.
        // Mainly returning an outcome/ status ? IActionResult is often suitable.
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
                    ReleaseYear = game.ReleaseYear
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
                ReleaseYear = game.ReleaseYear
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<GameResponse> CreateGame(CreateGameRequest request)
        {

            var gameAdd = new Game
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear,
            };

            var createdGame = _gameService.CreateGame(gameAdd);
            
            var response = new GameResponse
            {
                Id = createdGame.Id,
                Title = createdGame.Title,
                Genre = createdGame.Genre,
                ReleaseYear = createdGame.ReleaseYear,
            };

            return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, UpdateGameRequest request)
        {
            var updateGame = new Game
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear
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

