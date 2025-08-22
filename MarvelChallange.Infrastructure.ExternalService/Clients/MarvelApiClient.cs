namespace MarvelChallange.Infrastructure.ExternalService.Clients;

public class MarvelApiClient : IMarvelApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public MarvelApiClient(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<MarvelChallange.Core.Application.DTOs.MarvelDto> GetFullDataAsync()
    {
        var partialUrl = $"v1/public/characters?apikey={AppSettings.ExternalServices.MarvelApi.Apikey}&ts={AppSettings.ExternalServices.MarvelApi.Timestamp}&hash={AppSettings.ExternalServices.MarvelApi.Hash}";
        var response = await _httpClient.GetFromJsonAsync<MarvelDto>(partialUrl);

        var marvelDto = _mapper.Map<MarvelChallange.Core.Application.DTOs.MarvelDto>(response!);
        return marvelDto;
    }
}
