namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

public interface IIdentityProviderHealthService
{
    string GetProvider();

    bool Ping();
}
