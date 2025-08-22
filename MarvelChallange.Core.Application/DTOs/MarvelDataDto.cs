namespace MarvelChallange.Core.Application.DTOs;

public class MarvelDataDto
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public int Count { get; set; }
    public List<MarvelResultDto> Results { get; set; }
}
