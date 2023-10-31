namespace MarvelChallange.Domain.Models.External
{
    [Serializable]
    public class MarvelDataDto
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
        
        [JsonPropertyName("results")]
        public List<MarvelResultDto> Results { get; set; }
    }
}
