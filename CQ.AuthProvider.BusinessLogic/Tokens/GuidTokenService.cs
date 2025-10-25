using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Tokens;

public sealed class GuidTokenService(
    ISessionRepository sessionRepository
)
    : ITokenService
{
    public string AuthorizationTypeHandled => "Bearer";

    public Task<string> CreateAsync(object item)
    {
        return Task.FromResult(Db.NewId());
    }

    public Task<bool> IsValidAsync(string value)
    {
        var isGuid = Db.IsIdValid(value);

        return Task.FromResult(isGuid);
    }

    public async Task<object?> GetOrDefaultAsync(string value)
    {
        var session = await sessionRepository
            .GetByTokenAsync(value)
            .ConfigureAwait(false);

        var account = new AccountLogged(
            session.Account,
            value,
            session.App);

        return account;
    }
}
