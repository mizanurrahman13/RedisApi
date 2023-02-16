using RedisApi.Models;
using StackExchange.Redis;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace RedisApi.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public PlatformRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentOutOfRangeException(nameof(platform));

            var database = _connectionMultiplexer.GetDatabase();

            var serialPlatform = JsonSerializer.Serialize(platform);

            //database.StringSet(platform.Id, serialPlatform);
            //database.SetAdd("PlatformSet", serialPlatform);

            database.HashSet("hashplatform", new HashEntry[]
                {new HashEntry(platform.Id, serialPlatform)});
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var database = _connectionMultiplexer.GetDatabase();

            //var completeSet = database.SetMembers("PlatformSet");
            var completeHash = database.HashGetAll("hashplatform");

            if (completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

                return obj;
            }                

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var database = _connectionMultiplexer.GetDatabase();

            //var platform = database.StringGet(id);
            var platform = database.HashGet("hashplatform", id);

            if (!string.IsNullOrEmpty(platform))
                return JsonSerializer.Deserialize<Platform>(platform);

            return null;
        }
    }
}
