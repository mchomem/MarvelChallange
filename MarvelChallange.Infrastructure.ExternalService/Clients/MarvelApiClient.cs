namespace MarvelChallange.Infrastructure.ExternalService.Clients;

public class MarvelApiClient : IMarvelApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public MarvelApiClient(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<AppDto.MarvelDto> GetFullDataAsync()
    {
        var apiKey = _configuration.GetSection("ExternalServices:MarvelApi:Apikey").Value;
        var timeStamp = _configuration.GetSection("ExternalServices:MarvelApi:Timestamp").Value;
        var hash = _configuration.GetSection("ExternalServices:MarvelApi:Hash").Value;
        var partialUrl = $"v1/public/characters?apikey={apiKey}&ts={timeStamp}&hash={hash}";
        var response = await _httpClient.GetFromJsonAsync<InfraDto.MarvelDto>(partialUrl);
        var marvelDto = _mapper.Map<AppDto.MarvelDto>(response!);

        return marvelDto;
    }
}
