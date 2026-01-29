using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Suscriptions;

public interface ISuscriptionRepository
{
    Task<Suscription> CreateAsync(App app);

    Task<Suscription> GetByValueAsync(string value);
}