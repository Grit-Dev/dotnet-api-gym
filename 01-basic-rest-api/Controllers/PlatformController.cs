using BasicRestApi.Dtos;
using BasicRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public ActionResult<List<PlatformResponse>> GetPlatforms()
        {
            var platforms = _platformService.GetPlatforms();

            var response = new List<PlatformResponse>();

            foreach (var platform in platforms)
            {
                response.Add(new PlatformResponse
                {
                    Id = platform.Id,
                    Name = platform.Name,
                    Manufacturer = platform.Manufacturer,
                    ReleaseYear = platform.ReleaseYear
                });
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GameResponse> GetPlatformById(int id)
        {
            return Ok();
        }
    }
}
