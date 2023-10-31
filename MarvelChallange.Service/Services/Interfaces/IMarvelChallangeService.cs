namespace MarvelChallange.Service.Services.Interfaces
{
    public interface IMarvelChallangeService
    {
        public Task<string> AddToFile(MarvelDto marvelDto);
    }
}
