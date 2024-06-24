
namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public interface IResetPasswordService
{
    Task CreateAsync(string email);

    Task AcceptAsync(
        string id,
        AcceptResetPasswordArgs args);

    Task<ResetPassword> GetActiveByIdAsync(string id);
}
