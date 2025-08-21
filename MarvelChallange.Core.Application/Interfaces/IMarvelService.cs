namespace MarvelChallange.Core.Application.Interfaces;

public interface IMarvelService
{
    public Task<MarvelDto> GetFullDataAsync();

    public Task<string> ExportDataToFileAsync();

    public Task DeleteAllFilesAsync();
    public Task<string> AddToFileAsync(MarvelDto marvelDto);
}
