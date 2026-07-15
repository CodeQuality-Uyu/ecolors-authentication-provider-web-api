using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

public sealed record class Session()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Token { get; init; } = null!;

    public Account Account { get; init; } = null!;

    public App App { get; init; } = null!;

    /// <summary>
    /// App-specific data fetched from the app's own API at login time, when the
    /// app registered an <see cref="Apps.AccountDataSource"/>. Opaque to the auth
    /// provider — forwarded as-is in the login response. Null when the app has no
    /// source configured or the fetch failed.
    /// </summary>
    public object? AppData { get; init; }

    public Session(
    Account account,
    App app,
    string token)
        : this()
    {
        Account = account;
        App = app;
        Token = token;
    }
}
