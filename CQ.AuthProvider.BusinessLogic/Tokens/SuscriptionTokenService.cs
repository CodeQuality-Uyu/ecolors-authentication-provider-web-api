using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Subscriptions;

namespace CQ.AuthProvider.BusinessLogic.Tokens;

internal sealed class SubscriptionTokenService(ISubscriptionRepository subscriptionRepository)
    : ITokenService
{
    public string AuthorizationTypeHandled => "Subscription";

    public Task<string> CreateAsync(object item)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public async Task<object?> GetOrDefaultAsync(string value)
    {
        var subscription = await subscriptionRepository
            .GetByValueAsync(value)
            .ConfigureAwait(false);

        var subscriptionAccount = AccountLogged.NewSubscription(subscription.App, value);

        return subscriptionAccount;
    }

    public Task<bool> IsValidAsync(string value)
    {
        var isGuid = Guid.TryParse(value, out var guidValue);

        return Task.FromResult(isGuid);
    }
}