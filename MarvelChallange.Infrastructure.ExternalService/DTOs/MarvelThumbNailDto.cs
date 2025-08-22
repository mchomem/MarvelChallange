namespace MarvelChallange.Infrastructure.ExternalService.DTOs;

public class MarvelThumbNailDto
{
    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }
}
