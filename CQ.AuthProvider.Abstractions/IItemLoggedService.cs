namespace CQ.AuthProvider.Abstractions;

public interface IItemLoggedService
{
    Task<object> GetByHeaderAsync(string header, string value);
}
