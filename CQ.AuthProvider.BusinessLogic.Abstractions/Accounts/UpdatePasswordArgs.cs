using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts;
public readonly struct UpdatePasswordArgs
{
    public string OldPassword { get; init; }

    public string NewPassword { get; init; }

    public UpdatePasswordArgs(
        string oldPassword,
        string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
        Guard.ThrowIsInputInvalidPassword(newPassword, nameof(NewPassword));
    }
}
