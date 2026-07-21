using BasicRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Game>> GetGames()
        {
            // Returning data? Prefer ActionResult<T>.
            // Mainly returning an outcome/ status ? IActionResult is often suitable.
            var games = new List<Game>
            {
                new() { Id = 1, Title = "Witcher 3", Genre = "Action RPG", ReleaseYear = 2020},
                new() { Id = 2, Title = "Cyberpunk 2077", Genre = "Action RPG", ReleaseYear = 2020},
                new() { Id = 3, Title = "Crimson Desert", Genre = "Action RPG", ReleaseYear = 2020}
            };

            return Ok(games);
        }
    }
}
