using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Me;
public interface IMeService
{
    Task UpdatePasswordAsync(UpdatePasswordArgs args);

    Task TransferTenantAsync(
        string newOwnerId,
        AccountLogged accountLogged);
}
