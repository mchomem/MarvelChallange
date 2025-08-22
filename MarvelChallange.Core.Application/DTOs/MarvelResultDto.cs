namespace MarvelChallange.Core.Application.DTOs;

public class MarvelResultDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Modified { get; set; }
    public MarvelThumbNailDto Thumbnail { get; set; }
    public string ResourceURI { get; set; }
    public MarvelStructureDto Comics { get; set; }
    public MarvelStructureDto Series { get; set; }
    public MarvelStructureDto Stories { get; set; }
    public MarvelStructureDto Events { get; set; }
}
