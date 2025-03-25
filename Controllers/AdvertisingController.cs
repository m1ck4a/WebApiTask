using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisingController : Controller
    {
        private readonly AdvertisingService _advertisingService;

        public AdvertisingController(AdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        [HttpPost("load")]
        public IActionResult LoadData([FromBody] string filePath)
        {
            try
            {
                _advertisingService.LoadDataFromFile(filePath);
                return Ok("Data loaded successfully.");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("platforms")]
        public IActionResult GetPlatforms([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest("Location is required.");
            }

            var platforms = _advertisingService.GetPlatformsForLocation(location);
            return Ok(platforms);
        }
    }
}
