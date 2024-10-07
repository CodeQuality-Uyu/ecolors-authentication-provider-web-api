using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Me;

internal sealed class MeService(
    IAccountRepository _accountRepository,
    ITenantRepository _tenantRepository,
    IIdentityRepository _identityRepository,
    IRoleRepository _roleRepository,
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
            nameof(Account.Tenant),
            nameof(Account.Roles))
            .ConfigureAwait(false);

        if (newOwner.TenantValue.Id != accountLogged.TenantValue.Id)
        {
            throw new InvalidOperationException("New owner is not in tenant");
        }

        var isTenantOwner = newOwner.HasPermission(AuthConstants.TENANT_OWNER_ROLE_ID);
        if (isTenantOwner)
        {
            return;
        }

        await _tenantRepository
            .UpdateOwnerByIdAsync(
            accountLogged.TenantValue.Id,
            newOwner);

        await _accountRepository
            .AddRoleByIdAsync(newOwnerId, AuthConstants.TENANT_OWNER_ROLE_ID)
            .ConfigureAwait(false);

        var newRole = await _roleRepository
            .GetDefaultOrDefaultByAppIdAndTenantIdAsync(AuthConstants.AUTH_WEB_API_APP_ID, accountLogged.TenantValue.Id)
            .ConfigureAwait(false);
        if (Guard.IsNotNull(newRole))
        {
            await _accountRepository
                .AddRoleByIdAsync(accountLogged.Id, newRole.Id)
                .ConfigureAwait(false);
        }

        await _accountRepository
            .RemoveRoleByIdAsync(accountLogged.Id, AuthConstants.TENANT_OWNER_ROLE_ID)
            .ConfigureAwait(false);

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }

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
