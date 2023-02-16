using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedisApi.Data;
using RedisApi.Models;

namespace RedisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        private readonly IPlatformRepository _platformRepository;
        public PlatformController(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = _platformRepository.GetPlatformById(id);

            if (platform != null)
                return Ok(platform);

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _platformRepository.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, platform);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms() 
        {
            return Ok(_platformRepository.GetAllPlatforms());
        }
    }
}
