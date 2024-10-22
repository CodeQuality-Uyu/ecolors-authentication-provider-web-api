namespace CQ.AuthProvider.Abstractions;

public interface ITokenService
{
    Task<string> CreateAsync(object item);

    Task<bool> IsValidAsync(string header, string value);
}
