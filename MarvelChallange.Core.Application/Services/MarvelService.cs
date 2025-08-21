namespace MarvelChallange.Core.Application.Services;

public class MarvelService : IMarvelService
{
    private readonly IMarvelApiClient _marvelApiClient;

    public MarvelService(IMarvelApiClient marvelApiClient)
    {
        _marvelApiClient = marvelApiClient;
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
            string fullPath = AppSettings.FileExportData.FileOutputDirectory;

            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath, true);
        });
    }

    public async Task<string> AddToFileAsync(MarvelDto marvelDto)
    {
        DateTime dateTimeNow = DateTime.Now;
        string fullFileName = $"{AppSettings.FileExportData.FileOutputDirectory}/{AppSettings.FileExportData.FileName}.{dateTimeNow.ToString("dd.MM.yyyy HH.mm.ss")}.{AppSettings.FileExportData.FileExtension}";

        await Task.Run(() =>
        {
            if (!Directory.Exists(AppSettings.FileExportData.FileOutputDirectory))
                Directory.CreateDirectory(AppSettings.FileExportData.FileOutputDirectory);
        });

        using (StreamWriter sw = new StreamWriter(fullFileName, true))
        {
            await sw.WriteLineAsync($"File generated in {dateTimeNow.ToString("dd/MM/yyyy HH:mm:ss")}");
            await sw.WriteLineAsync("");

            foreach (MarvelResultDto result in marvelDto.Data.Results)
            {
                int maxSeparator = 200;
                await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                await sw.WriteLineAsync($"ID: {result.Id}");
                await sw.WriteLineAsync($"Name: {result.Name}");
                await sw.WriteLineAsync($"Description: {result.Description}");

                await sw.WriteLineAsync("Comics (names):");
                await Task.Run(() => result.Comics.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync("");

                await sw.WriteLineAsync($"Series (names):");
                await Task.Run(() => result.Series.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync("");

                await sw.WriteLineAsync($"Stories (names):");
                await Task.Run(() => result.Stories.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync("");

                await sw.WriteLineAsync($"Events (names):");
                await Task.Run(() => result.Events.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                await sw.WriteLineAsync("");

                await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                await sw.WriteLineAsync("");
            }
        }

        return fullFileName;
    }
}
