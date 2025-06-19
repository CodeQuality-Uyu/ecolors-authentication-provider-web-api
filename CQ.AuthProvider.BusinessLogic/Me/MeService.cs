using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;

namespace CQ.AuthProvider.BusinessLogic.Me;

internal sealed class MeService(
    IIdentityRepository _identityRepository)
    : IMeService
{
    public async Task UpdatePasswordLoggedAsync(
        string oldPassword,
        string newPassword,
        AccountLogged accountLogged)
    {
        await _identityRepository
            .UpdatePasswordByIdAsync(
            accountLogged.Id,
            oldPassword,
            newPassword)
            .ConfigureAwait(false);
    }
}
