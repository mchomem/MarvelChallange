namespace MarvelChallange.Api.Controllers;

/// <summary>
/// Controller responsible for handling requests related to Marvel API data retrieval, file export, and file deletion.
/// </summary>
[ApiController]
[Route("[controller]")]
public class MarvelChallengeController : ControllerBase
{
    private readonly IMarvelService _marvelService;
    private readonly ILogger<MarvelChallengeController> _logger;
    private readonly string _defaultTimeFormat = "dd/MM/yyyy HH:mm:ss";

    /// <summary>
    /// Constructor for MarvelChallengeController. Initializes the logger and Marvel service dependencies.
    /// </summary>
    /// <param name="logger">The logger instance for logging information and errors.</param>
    /// <param name="marvelSerice">The Marvel service instance for handling Marvel API data operations.</param>
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
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: GET method. Action taken.";
            _logger.LogInformation(logMessage);
            var marvelDto = await _marvelService.GetFullDataAsync();

            return Ok(marvelDto);
        }
        catch (Exception e)
        {
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: GET method. Action taken. ERROR: {e.Message}";
            _logger.LogError(e, logMessage);
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
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: POST method. Action taken.";
            _logger.LogInformation(logMessage);
            var message = new { message = $"Generated text file at {fullPath}" };
            return Ok(message);
        }
        catch (Exception e)
        {
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: POST method. Action taken. ERROR: {e.Message}";
            _logger.LogError(e, logMessage);
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
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: DELETE method. Action taken.";
            _logger.LogInformation(logMessage);
            await _marvelService.DeleteAllFilesAsync();
            return Ok(new { message = "Export files deleted." });
        }
        catch (Exception e)
        {
            var logMessage = $"[{DateTime.Now.ToString(_defaultTimeFormat)}]: DELETE method. Action taken. ERROR: {e.Message}";
            _logger.LogError(e, logMessage);
            return StatusCode(500, e.Message);
        }
    }
}
