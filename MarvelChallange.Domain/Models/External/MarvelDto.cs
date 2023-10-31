namespace MarvelChallange.Domain.Models.External
{
    [Serializable]
    public class MarvelDto
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("attributionText")]
        public string AttributionText { get; set; }

        [JsonPropertyName("AttributionHTML")]
        public string attributionHTML { get; set; }

        [JsonPropertyName("etag")]
        public string Etag { get; set; }

        [JsonPropertyName("data")]
        public MarvelDataDto Data { get; set; }
    }
}
