namespace MarvelChallange.Infrastructure.ExternalService.DTOs;

public class MarveltemsDto
{
    [JsonPropertyName("resourceURI")]
    public string ResourceURI { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
