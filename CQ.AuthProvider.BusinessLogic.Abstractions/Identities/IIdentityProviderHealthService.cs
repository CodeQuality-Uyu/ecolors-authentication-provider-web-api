namespace CQ.AuthProvider.BusinessLogic.Identities;

public interface IIdentityProviderHealthService
{
    string GetProvider();

    bool Ping();
}
