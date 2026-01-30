using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Subscriptions;

public interface ISubscriptionRepository
{
    Task<Subscription> CreateAsync(App app);

    Task<Subscription> GetByValueAsync(string value);
}