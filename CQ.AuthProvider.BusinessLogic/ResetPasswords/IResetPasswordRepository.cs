namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public interface IResetPasswordRepository
{
    Task<ResetPassword> GetByIdAsync(string id);

    Task<ResetPassword?> GetOrDefaultByEmailAsync(string email);

    Task CreateAndSaveAsync(ResetPassword resetPassword);

    Task DeletePendingAsync(
        string id,
        string code);

    Task UpdateCodeByIdAsync(
        string id,
        string code);
}
