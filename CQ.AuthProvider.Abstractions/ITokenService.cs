namespace CQ.AuthProvider.Abstractions;

public interface ITokenService
{
    Task<string> CreateAsync(object item);

    Task<bool> IsValidAsync(string value);
}

public interface IBearerTokenService : ITokenService
{
}
