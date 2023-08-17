using MarvelChallange.Domain.Models.External;
using MarvelChallange.Service.Services.External.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarvelChallange.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelChallengeController : ControllerBase
    {
        private readonly IMarvelService _marvelService;
        private readonly ILogger<MarvelChallengeController> _logger;

        public MarvelChallengeController(
            ILogger<MarvelChallengeController> logger,
            IMarvelService marvelSerice)
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
                _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: GET method. Action taken.");
                return Ok(await _marvelService.GetFullData());
            }
            catch (Exception e)
            {
                _logger.LogError($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: GET method. Action taken. ERROR: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Writes id, name, description and name only reports for comics, series, stories and events to a text file.
        /// </summary>
        /// <returns></returns>
        [HttpPost("export-to-file")]
        public async Task<ActionResult<string>> Post()
        {
            try
            {
                await _marvelService.ExportDataToFile();
                _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: POST method. Action taken.");
                return Ok(new { message = "Generated text file." });
            }
            catch (Exception e)
            {
                _logger.LogError($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: GET method. Action taken. ERROR: {e.Message}");
                return StatusCode(500, e.Message);
            }            
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: DELETE method. Action taken.");
                await _marvelService.DeleteAllFiles();
                return Ok(new { message = "Export files excluded." });
            }
            catch (Exception e)
            {
                _logger.LogError($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: DELETE method. Action taken. ERROR: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }
    }
}