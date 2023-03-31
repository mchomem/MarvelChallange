using System.Text.Json.Serialization;

namespace MarvelChallange.Domain.Models.External
{
    [Serializable]
    public class MarvelStructureDto
    {
        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonPropertyName("items")]
        public List<MarveltemsDto> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }
}
