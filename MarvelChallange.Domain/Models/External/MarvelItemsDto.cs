namespace MarvelChallange.Domain.Models.External
{
    [Serializable]
    public class MarveltemsDto
    {
        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
