using MarvelChallange.Domain.Models;
using MarvelChallange.Domain.Models.External;
using MarvelChallange.Service.Services.Interfaces;
using System.Text.Json;

namespace MarvelChallange.Service.Services.External
{
    public class MarvelService : ExternalBaseService
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

        public async Task ExportDataToFile()
        {
            MarvelDto? marvelJson = await GetFullData();

            if (marvelJson == null)
                throw new Exception("No data to export");

            await _marvelChallangeService.AddToFile(marvelJson);
        }
    }
}
