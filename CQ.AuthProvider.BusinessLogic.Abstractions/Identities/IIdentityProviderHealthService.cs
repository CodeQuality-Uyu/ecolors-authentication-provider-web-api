namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities
{
    public interface IIdentityProviderHealthService
    {
        string GetProvider();

        string GetName();

        bool Ping();
    }
}
