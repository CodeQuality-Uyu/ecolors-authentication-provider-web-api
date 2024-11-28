namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public interface IResetPasswordService
{
    Task CreateAsync(string email);

    Task AcceptAsync(
        Guid id,
        AcceptResetPasswordArgs args);
}
