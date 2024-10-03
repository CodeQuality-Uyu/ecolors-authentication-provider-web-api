using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts;
public readonly struct UpdatePasswordArgs
{
    public string Email { get; init; }

    public string OldPassword { get; init; }

    public string NewPassword { get; init; }

    public UpdatePasswordArgs(
        string email,
        string oldPassword,
        string newPassword)
    {
        Guard.ThrowIsInputInvalidEmail(email);
        Email = email;

        OldPassword = oldPassword;
        NewPassword = newPassword;
        Guard.ThrowIsInputInvalidPassword(newPassword, nameof(NewPassword));
    }
}
