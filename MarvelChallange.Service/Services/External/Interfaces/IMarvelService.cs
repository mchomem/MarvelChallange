namespace MarvelChallange.Service.Services.External.Interfaces;

public interface IMarvelService
{
    public Task<MarvelDto?> GetFullData();

    public Task<string> ExportDataToFile();

    public Task DeleteAllFiles();
}
