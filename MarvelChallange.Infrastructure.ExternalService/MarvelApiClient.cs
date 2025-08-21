namespace MarvelChallange.Infrastructure.ExternalService;

public class MarvelApiClient : IMarvelApiClient
{
    private readonly HttpClient _httpClient;

    public MarvelApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MarvelDto> GetFullDataAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<MarvelDto>($"v1/public/characters?apikey={AppSettings.ExternalServices.MarvelApi.Apikey}&ts={AppSettings.ExternalServices.MarvelApi.Timestamp}&hash={AppSettings.ExternalServices.MarvelApi.Hash}");
        return response!;
    }
}
