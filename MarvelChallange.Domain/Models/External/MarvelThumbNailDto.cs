namespace MarvelChallange.Domain.Models.External;

[Serializable]
public class MarvelThumbNailDto
{
    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }
}
