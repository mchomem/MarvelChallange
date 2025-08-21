namespace MarvelChallange.Core.Domain.Exceptions.Marvel;

public class MarvelException : BusinessException
{
    public MarvelException(string message) : base(message) { }
}
