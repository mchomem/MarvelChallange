namespace MarvelChallange.Core.Application.DTOs;

public class MarvelDto
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Copyright { get; set; }
    public string AttributionText { get; set; }
    public string attributionHTML { get; set; }
    public string Etag { get; set; }
    public MarvelDataDto Data { get; set; }
}
