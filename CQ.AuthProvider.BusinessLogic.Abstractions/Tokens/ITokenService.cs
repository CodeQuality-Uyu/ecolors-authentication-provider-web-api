namespace CQ.AuthProvider.BusinessLogic.Tokens
{
    public interface ITokenService
    {
        string Create();

        Task<bool> IsValidAsync(string token);
    }
}
