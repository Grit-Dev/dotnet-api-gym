using Microsoft.AspNetCore.Mvc;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> GetGames()
        {
            // Returning data? Prefer ActionResult<T>.
            // Mainly returning an outcome/ status ? IActionResult is often suitable.
            var games = new List<string>
            {
                "Cyberpunk 2077",
                "The Witcher 3",
                "Crimson Desert"
            };

            return Ok(games);
        }
    }
}
