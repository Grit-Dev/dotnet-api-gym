using BasicRestApi.Data;
using BasicRestApi.Models;

namespace BasicRestApi.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly GameDbContext _context;
        public PlatformService(GameDbContext dbContext)
        {
            _context = dbContext;
        }

        public Platform? GetPlatormById(int id) => _context.Platforms.Find(id);

        public Platform CreatePlatform(Platform platform)
        {
            var createPlatform = new Platform
            {
                Name = platform.Name,
                Manufacturer = platform.Manufacturer,
                ReleaseYear = platform.ReleaseYear
            };

            _context.Platforms.Add(createPlatform);
            _context.SaveChanges();

            return createPlatform;
        }

        public bool DeletePlatform(int id)
        {
            var platform = _context.Platforms.Find(id);

            if (platform is null)
            {
                return false;
            }

            _context.Platforms.Remove(platform);
            _context.SaveChanges();

            return true;
        }

        public IReadOnlyList<Platform> GetPlatforms()
        {
            return _context.Platforms.ToList();

        }

        public bool UpdatePlatform(int id, Platform platform)
        {
            var updatePlatform = _context.Platforms.Find(id);

            if (updatePlatform is null)
            {
                return false;
            }

            updatePlatform.Name = platform.Name;
            updatePlatform.Manufacturer = platform.Manufacturer;
            updatePlatform.ReleaseYear = platform.ReleaseYear;

            _context.SaveChanges();

            return true;
        }
    }
}
