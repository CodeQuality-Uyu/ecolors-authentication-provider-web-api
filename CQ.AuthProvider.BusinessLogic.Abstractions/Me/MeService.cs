using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Me;

internal sealed class MeService(
    IAccountRepository _accountRepository,
    ITenantRepository _tenantRepository,
    IIdentityRepository _identityRepository,
    IUnitOfWork _unitOfWork)
    : IMeService
{
    public async Task TransferTenantAsync(
        string newOwnerId,
        AccountLogged accountLogged)
    {
        var newOwner = await _accountRepository
            .GetByIdAsync(
            newOwnerId,
            [nameof(Account.Tenant)])
            .ConfigureAwait(false);

        if (newOwner.Tenant.Id != accountLogged.Tenant.Id)
        {
            throw new InvalidOperationException("New owner is not in tenant");
        }

        await _tenantRepository
            .UpdateOwnerByIdAsync(
            accountLogged.Tenant.Id,
            newOwner);

        var notTenantOwner = !newOwner.HasPermission(AuthConstants.TENANT_OWNER_ROLE_ID);
        if (notTenantOwner)
        {
            await _accountRepository
                .AddRoleByIdAsync(
                newOwnerId,
                AuthConstants.TENANT_OWNER_ROLE_ID)
                .ConfigureAwait(false);
        }

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task UpdatePasswordAsync(UpdatePasswordArgs args)
    {
        var identity = await _identityRepository
            .GetByCredentialsAsync(
            args.Email,
            args.OldPassword)
            .ConfigureAwait(false);

        await _identityRepository
            .UpdatePasswordByIdAsync(
            identity.Id,
            args.NewPassword)
            .ConfigureAwait(false);
    }
}
