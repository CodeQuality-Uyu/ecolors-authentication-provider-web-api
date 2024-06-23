using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public sealed record class SessionCreated
{
    public readonly Account Account;

    public readonly string Token;

    public SessionCreated(
        Account account,
        string token)
    {
        Account = account;
        Token = token;
    }
}
