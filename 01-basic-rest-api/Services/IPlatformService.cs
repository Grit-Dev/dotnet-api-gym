using BasicRestApi.Models;

namespace BasicRestApi.Services
{
    public interface IPlatformService
    {
        Platform? GetPlatormById(int id);

        Platform? CreatePlatform(Platform platform);

        bool DeletePlatform(int id);

        IReadOnlyList<Platform> GetPlatforms();

        bool UpdatePlatform(int id, Platform platform);


    }
}