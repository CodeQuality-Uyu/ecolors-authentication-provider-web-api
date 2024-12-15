namespace CQ.AuthProvider.Abstractions;

public interface IItemLoggedService
{
    string AuthorizationTypeHandled { get; }

    Task<object> GetByHeaderAsync(string value);
}