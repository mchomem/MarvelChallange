namespace MarvelChallange.Service.Services.External
{
    public class MarvelService : ExternalBaseService, IMarvelService
    {
        private readonly IMarvelChallangeService _marvelChallangeService;

        public MarvelService(IMarvelChallangeService marvelChallangeService)
            => _marvelChallangeService = marvelChallangeService;        

        public async Task<MarvelDto?> GetFullData()
        {
            string url = $"{AppSettings.ExternalServices.MarvelApi.UrlBase}/v1/public/characters?apikey={AppSettings.ExternalServices.MarvelApi.Apikey}&ts={AppSettings.ExternalServices.MarvelApi.Timestamp}&hash={AppSettings.ExternalServices.MarvelApi.Hash}";
            string result = await SendRequest(url);
            MarvelDto? marvelJson = JsonSerializer.Deserialize<MarvelDto>(result);

            return marvelJson ?? null;
        }

        public async Task<string> ExportDataToFile()
        {
            MarvelDto? marvelJson = await GetFullData();

            if (marvelJson == null)
                throw new Exception("No data to export");

            return await _marvelChallangeService.AddToFile(marvelJson);
        }

        public async Task DeleteAllFiles()
        {
            await Task.Run(() =>
            {
                string fullPath = AppSettings.FileExportData.FileOutputDirectory;

                if (Directory.Exists(fullPath))
                    Directory.Delete(fullPath, true);
            });
        }
    }
}
