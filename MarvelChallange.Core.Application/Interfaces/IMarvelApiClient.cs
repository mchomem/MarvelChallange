namespace MarvelChallange.Core.Application.Interfaces;

public interface IMarvelApiClient
{
    Task<MarvelDto> GetFullDataAsync();
}
