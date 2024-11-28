namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public interface IResetPasswordRepository
{
    Task<ResetPassword> GetByIdAsync(Guid id);

    Task<ResetPassword?> GetOrDefaultByEmailAsync(string email);

    Task CreateAndSaveAsync(ResetPassword resetPassword);

    Task DeletePendingAsync(
        Guid id,
        string code);

    Task UpdateCodeByIdAsync(
        Guid id,
        string code);
}
