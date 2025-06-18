namespace CQ.AuthProvider.BusinessLogic.Accounts;
public sealed record UpdatePasswordArgs(
    string OldPassword,
    string NewPassword);
