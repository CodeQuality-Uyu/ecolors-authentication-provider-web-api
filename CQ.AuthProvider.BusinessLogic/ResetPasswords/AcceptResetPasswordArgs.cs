namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public sealed record AcceptResetPasswordArgs(
    string Email,
    string NewPassword,
    int Code);
