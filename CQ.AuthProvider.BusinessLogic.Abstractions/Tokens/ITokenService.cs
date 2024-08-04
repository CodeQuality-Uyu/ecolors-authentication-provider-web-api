namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tokens
{
    public interface ITokenService
    {
        string Create();

        Task<bool> IsValidAsync(string token);
    }
}
