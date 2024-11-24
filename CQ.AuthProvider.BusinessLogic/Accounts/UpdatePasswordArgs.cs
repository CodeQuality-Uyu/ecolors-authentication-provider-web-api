namespace CQ.AuthProvider.BusinessLogic.Accounts;
public sealed record UpdatePasswordArgs(
    string Email,
    string OldPassword,
    string NewPassword);
