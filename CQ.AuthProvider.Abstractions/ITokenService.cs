namespace CQ.AuthProvider.Abstractions;

public interface ITokenService
{
    string AuthorizationTypeHandled { get; }

    Task<string> CreateAsync(object item);

    Task<bool> IsValidAsync(string value);
}