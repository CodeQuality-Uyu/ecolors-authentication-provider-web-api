using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Me;

internal sealed class MeService(
    IIdentityRepository _identityRepository)
    : IMeService
{
    public async Task UpdatePasswordLoggedAsync(
        string newPassword,
        AccountLogged accountLogged)
    {
        await _identityRepository
            .UpdatePasswordByIdAsync(
            accountLogged.Id,
            newPassword)
            .ConfigureAwait(false);
    }
}
