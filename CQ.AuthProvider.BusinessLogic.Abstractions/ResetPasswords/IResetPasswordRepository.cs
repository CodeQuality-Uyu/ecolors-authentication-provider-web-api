
namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;

internal interface IResetPasswordRepository
{
    Task<ResetPassword> GetByIdAsync(string id);

    Task UpdateStatusByIdAsync(
        string id,
        ResetPasswordStatus status);

    Task<ResetPassword> GetByEmailOfAccountAsync(string email);

    Task CreateAsync(ResetPassword resetPassword);

    Task UpdateCodeByIdAsync(
        string id,
        string code);

    Task<ResetPassword> GetActiveByIdAsync(string id);
}
