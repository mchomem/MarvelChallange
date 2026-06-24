namespace MarvelChallange.Core.Application.Services;

public class MarvelService : IMarvelService
{
    private readonly IMarvelApiClient _marvelApiClient;
    private readonly IConfiguration _configuration;
    private readonly string _fileOutputDirectory = "FileExportData:FileOutputDirectory";

    public MarvelService(IMarvelApiClient marvelApiClient, IConfiguration configuration)
    {
        _marvelApiClient = marvelApiClient;
        _configuration = configuration;
    }

    public async Task<MarvelDto> GetFullDataAsync()
    {
        var marvelDto = await _marvelApiClient.GetFullDataAsync();
        return marvelDto;
    }

    public async Task<string> ExportDataToFileAsync()
    {
        MarvelDto marvelJson = await GetFullDataAsync();

        if (marvelJson is null)
            throw new MarvelException("No data to export");

        var result = await AddToFileAsync(marvelJson);
        return result;
    }

    public async Task DeleteAllFilesAsync()
    {
        await Task.Run(() =>
        {
            string fullPath = _configuration.GetSection(_fileOutputDirectory).Value!;

            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath, true);
        });
    }

    public async Task<string> AddToFileAsync(MarvelDto marvelDto)
    {
        DateTime dateTimeNow = DateTime.Now;
        var fileOutputDirectory = _configuration.GetSection(_fileOutputDirectory).Value;
        var fileName = _configuration.GetSection("FileExportData:FileName").Value;
        var fileExtension = _configuration.GetSection("FileExportData:FileExtension").Value;
        string fullFileName = $"{fileOutputDirectory}/{fileName}.{dateTimeNow.ToString("dd.MM.yyyy HH.mm.ss")}.{fileExtension}";

        await Task.Run(() =>
        {
            if (!Directory.Exists(_configuration.GetSection(_fileOutputDirectory).Value))
                Directory.CreateDirectory(_configuration.GetSection(_fileOutputDirectory).Value!);
        });

        using (StreamWriter sw = new StreamWriter(fullFileName, true))
        {
            await sw.WriteLineAsync($"File generated in {dateTimeNow.ToString("dd/MM/yyyy HH:mm:ss")}");
            await sw.WriteLineAsync(string.Empty);

            foreach (MarvelResultDto result in marvelDto.Data.Results)
            {
                int maxSeparator = 200;
                await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                await sw.WriteLineAsync($"ID: {result.Id}");
                await sw.WriteLineAsync($"Name: {result.Name}");
                await sw.WriteLineAsync($"Description: {result.Description}");

                await sw.WriteLineAsync("Comics (names):");
                await Task.Run(() => result.Comics.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync(string.Empty);

                await sw.WriteLineAsync($"Series (names):");
                await Task.Run(() => result.Series.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync(string.Empty);

                await sw.WriteLineAsync($"Stories (names):");
                await Task.Run(() => result.Stories.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync(string.Empty);

                await sw.WriteLineAsync($"Events (names):");
                await Task.Run(() => result.Events.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync(string.Empty);

                await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                await sw.WriteLineAsync(string.Empty);
            }
        }

        return fullFileName;
    }
}
