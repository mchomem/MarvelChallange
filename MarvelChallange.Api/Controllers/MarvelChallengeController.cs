namespace MarvelChallange.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MarvelChallengeController : ControllerBase
{
    private readonly IMarvelService _marvelService;
    private readonly ILogger<MarvelChallengeController> _logger;

    public MarvelChallengeController(ILogger<MarvelChallengeController> logger, IMarvelService marvelSerice)
    {
        _logger = logger;
        _marvelService = marvelSerice;
    }

    /// <summary>
    /// Get all character data from marvel api.
    /// </summary>
    /// <returns>Returns the filled json object.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: GET method. Action taken.");
            var marvelDto = await _marvelService.GetFullDataAsync();

            return Ok(marvelDto);
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
    public async Task<IActionResult> PostAsync()
    {
        try
        {
            string fullPath = await _marvelService.ExportDataToFileAsync();
            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: POST method. Action taken.");
            var message = new { message = $"Generated text file at {fullPath}" };
            return Ok(message);
        }
        catch (Exception e)
        {
            _logger.LogError($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: GET method. Action taken. ERROR: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Delete all exported files.
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: DELETE method. Action taken.");
            await _marvelService.DeleteAllFilesAsync();
            return Ok(new { message = "Export files deleted." });
        }
        catch (Exception e)
        {
            _logger.LogError($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: DELETE method. Action taken. ERROR: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }
}
