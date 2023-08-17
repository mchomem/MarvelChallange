using MarvelChallange.Domain.Models.External;
using MarvelChallange.Service.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace MarvelChallange.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelChallengeController : ControllerBase
    {
        private readonly MarvelService _marvelService;
        private readonly ILogger<MarvelChallengeController> _logger;

        public MarvelChallengeController(
            ILogger<MarvelChallengeController> logger,
            MarvelService marvelSerice)
        {
            _logger = logger;
            _marvelService = marvelSerice;
        }

        /// <summary>
        /// Get all character data from marvel api.
        /// </summary>
        /// <returns>Returns the filled json object.</returns>
        [HttpGet]
        public async Task<ActionResult<MarvelDto>> Get()
        {
            try
            {
                return Ok(await _marvelService.GetFullData());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Writes id, name, description and name only reports for comics, series, stories and events to a text file.
        /// </summary>
        /// <returns></returns>
        [HttpPost("import")]
        public async Task<ActionResult<string>> Post()
        {
            try
            {
                await _marvelService.ImportData();
                return Ok(new { message = "Generated text file." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }            
        }
    }
}