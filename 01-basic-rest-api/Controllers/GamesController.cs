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
        public ActionResult<List<Game>> GetGames()
        {

            return Ok(_games);
        }

        [HttpGet("{id}")]
        public ActionResult<Game> GetGameById(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);

            if (game is null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        public ActionResult<Game> CreateGame(Game game)
        {
            var newId = _games.Count == 0 ? 1 : _games.Max(g => g.Id) + 1;

            var gameAdd = new Game
            {
                Id = newId,
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
            };

            _games.Add(gameAdd);

            return CreatedAtAction(nameof(GetGameById), new { id = gameAdd.Id }, gameAdd);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, Game game)
        {
            var gameFound = _games.FirstOrDefault(g => g.Id == id);

            if (gameFound is null)
            {
                return NotFound();
            }

            gameFound.Title = game.Title;
            gameFound.Genre = game.Genre;
            gameFound.ReleaseYear = game.ReleaseYear;

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

