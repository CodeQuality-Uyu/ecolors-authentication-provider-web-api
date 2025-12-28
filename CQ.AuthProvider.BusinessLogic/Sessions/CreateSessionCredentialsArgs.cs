namespace CQ.AuthProvider.BusinessLogic.Sessions;

public sealed record CreateSessionCredentialsArgs(
    string Email,
    string Password,
    Guid AppId);
