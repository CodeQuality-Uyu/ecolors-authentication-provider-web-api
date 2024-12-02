namespace CQ.AuthProvider.BusinessLogic.Accounts;
public sealed record UpdatePasswordArgs(
    string Email,
    string Code,
    string NewPassword);
