using RedisApi.Models;

namespace RedisApi.Data
{
    public interface IPlatformRepository
    {
        void CreatePlatform(Platform platform);
        Platform? GetPlatformById(string id);
        IEnumerable<Platform?>? GetAllPlatforms();
    }
}
