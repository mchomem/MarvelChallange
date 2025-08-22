namespace MarvelChallange.Core.Application.Interfaces;

public interface IMarvelApiClient
{
    Task<MarvelChallange.Core.Application.DTOs.MarvelDto> GetFullDataAsync();
}
