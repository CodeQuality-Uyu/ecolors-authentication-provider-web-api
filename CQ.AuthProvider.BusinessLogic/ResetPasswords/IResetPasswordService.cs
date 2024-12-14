namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public interface IResetPasswordService
{
    Task CreateAsync(CreateResetPasswordArgs args);

    Task AcceptAsync(
        Guid id,
        AcceptResetPasswordArgs args);
}
