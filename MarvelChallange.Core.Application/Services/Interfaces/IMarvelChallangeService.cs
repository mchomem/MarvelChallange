namespace MarvelChallange.Core.Application.Services.Interfaces;

public interface IMarvelChallangeService
{
    public Task<string> AddToFile(MarvelDto marvelDto);
}
