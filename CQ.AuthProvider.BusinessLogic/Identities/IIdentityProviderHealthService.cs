namespace CQ.AuthProvider.BusinessLogic.Identities
{
    public interface IIdentityProviderHealthService
    {
        string GetProvider();

        string GetName();

        bool Ping();
    }
}
