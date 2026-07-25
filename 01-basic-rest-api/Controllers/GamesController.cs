using BasicRestApi.DTOS;
using BasicRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private static readonly List<Game> _games =
        [
            new() { Id = 1, Title = "Witcher 3", Genre = "Action RPG", ReleaseYear = 2020},
            new() { Id = 2, Title = "Cyberpunk 2077", Genre = "Action RPG", ReleaseYear = 2020},
            new() { Id = 3, Title = "Crimson Desert", Genre = "Action RPG", ReleaseYear = 2020}
        ];

        [HttpGet]
        // Returning data? Prefer ActionResult<T>.
        // Mainly returning an outcome/ status ? IActionResult is often suitable.
        public ActionResult<List<GameResponse>> GetGames()
        {
            var response = new List<GameResponse>();

            foreach (Game game in _games)
            {
                response.Add(new GameResponse
                {
                    Id = game.Id,
                    Title = game.Title,
                    Genre = game.Genre,
                    ReleaseYear = game.ReleaseYear,
                });
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GameResponse> GetGameById(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);

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
            var newId = _games.Count == 0 ? 1 : _games.Max(g => g.Id) + 1;

            var gameAdd = new Game
            {
                Id = newId,
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear,
            };

            _games.Add(gameAdd);

            var response = new GameResponse
            {
                Id = gameAdd.Id,
                Title = gameAdd.Title,
                Genre = gameAdd.Genre,
                ReleaseYear = gameAdd.ReleaseYear,
            };

            return CreatedAtAction(nameof(GetGameById), new { id = gameAdd.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, UpdateGameRequest request)
        {
            var gameFound = _games.FirstOrDefault(g => g.Id == id);

            if (gameFound is null)
            {
                return NotFound();
            }

            gameFound.Title = request.Title;
            gameFound.Genre = request.Genre;
            gameFound.ReleaseYear = request.ReleaseYear;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var gameFound = _games.FirstOrDefault(g => g.Id == id);

            if (gameFound is null)
            {
                return NotFound();
            }

            _games.Remove(gameFound);

            return NoContent();
        }
    }
}

