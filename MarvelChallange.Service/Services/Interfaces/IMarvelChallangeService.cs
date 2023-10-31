using MarvelChallange.Domain.Models;
using MarvelChallange.Domain.Models.External;

namespace MarvelChallange.Service.Services.Interfaces
{
    public interface IMarvelChallangeService
    {
        public Task<string> AddToFile(MarvelDto marvelDto);
    }
}
