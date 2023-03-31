using System.Text.Json.Serialization;

namespace MarvelChallange.Domain.Models.External
{
    [Serializable]
    public class MarvelResultDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("modified")]
        public string Modified { get; set; }

        [JsonPropertyName("thumbnail")]
        public MarvelThumbNailDto Thumbnail { get; set; }

        [JsonPropertyName("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonPropertyName("comics")]
        public MarvelStructureDto Comics { get; set; }

        [JsonPropertyName("series")]
        public MarvelStructureDto Series { get; set; }

        [JsonPropertyName("stories")]
        public MarvelStructureDto Stories { get; set; }

        [JsonPropertyName("events")]
        public MarvelStructureDto Events { get; set; }
    }
}
