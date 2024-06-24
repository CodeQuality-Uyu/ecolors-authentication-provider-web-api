using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;

public readonly struct AcceptResetPasswordArgs
{
    public string NewPassword { get; init; }

    public string Code { get; init; }

    public AcceptResetPasswordArgs(
        string newPassword,
        string code)
    {
        NewPassword = Guard.Encode(newPassword, nameof(newPassword));
        Guard.ThrowIsInputInvalidPassword(NewPassword);

        Code = Guard.Encode(code, nameof(code));
    }
}
