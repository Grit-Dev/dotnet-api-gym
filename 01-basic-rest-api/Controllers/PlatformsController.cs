using BasicRestApi.Dtos;
using BasicRestApi.Models;
using BasicRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        public PlatformsController(IPlatformService platformService)
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
        public ActionResult<PlatformResponse> GetPlatformById(int id)
        {
            var platform = _platformService.GetPlatormById(id);

            if (platform is null)
            {
                return NotFound();
            }

            var response = new PlatformResponse
            {
                Id = platform.Id,
                Name = platform.Name,
                Manufacturer = platform.Manufacturer,
                ReleaseYear = platform.ReleaseYear
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<PlatformResponse> CreatePlatform(CreatePlatformRequest request)
        {
            var platformAdd = new Platform
            {
                Name = request.Name,
                Manufacturer = request.Manufacturer,
                ReleaseYear = request.ReleaseYear
            };

            var createdPlatform = _platformService.CreatePlatform(platformAdd);

            var response = new PlatformResponse
            {
                Id = createdPlatform.Id,
                Name = createdPlatform.Name,
                Manufacturer = createdPlatform.Manufacturer,
                ReleaseYear = createdPlatform.ReleaseYear
            };

            return CreatedAtAction(nameof(GetPlatformById), new { id = createdPlatform.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlatform(int id, UpdatePlatformRequest request)
        {
            var platform = new Platform
            {
                Name = request.Name,
                Manufacturer = request.Manufacturer,
                ReleaseYear = request.ReleaseYear
            };

            var platformFound = _platformService.UpdatePlatform(id, platform);

            if (!platformFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlatform(int id)
        {
            var platformFound = _platformService.DeletePlatform(id);

            if (!platformFound)
            {
                return NotFound(); 
            }

            return NoContent();
        }
    }
}
