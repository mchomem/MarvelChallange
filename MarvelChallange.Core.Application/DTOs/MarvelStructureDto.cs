namespace MarvelChallange.Core.Application.DTOs;

public class MarvelStructureDto
{
    public int Available { get; set; }
    public string CollectionURI { get; set; }
    public List<MarveltemsDto> Items { get; set; }
    public int Returned { get; set; }
}
