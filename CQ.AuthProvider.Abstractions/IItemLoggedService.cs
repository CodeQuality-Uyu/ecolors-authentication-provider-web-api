namespace CQ.AuthProvider.Abstractions;

public interface IItemLoggedService
{
    Task<object> GetByHeaderAsync(string value);
}

public interface IBearerLoggedService : IItemLoggedService
{
}