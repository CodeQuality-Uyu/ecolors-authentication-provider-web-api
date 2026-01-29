using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Suscriptions;

namespace CQ.AuthProvider.BusinessLogic.Tokens;

internal sealed class SuscriptionTokenService(ISuscriptionRepository suscriptionRepository)
    : ITokenService
{
    public string AuthorizationTypeHandled => "Suscription";

    public Task<string> CreateAsync(object item)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public async Task<object?> GetOrDefaultAsync(string value)
    {
        var suscription = await suscriptionRepository
            .GetByValueAsync(value)
            .ConfigureAwait(false);

        var suscriptionAccount = AccountLogged.NewSuscription(suscription.App, value);

        return suscriptionAccount;
    }

    public Task<bool> IsValidAsync(string value)
    {
        var isGuid = Guid.TryParse(value, out var guidValue);

        return Task.FromResult(isGuid);
    }
}