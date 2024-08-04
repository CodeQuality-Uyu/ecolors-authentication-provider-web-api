using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

internal sealed class InvitationService(
    IInvitationRepository invitationRepository)
    : IInvitationService
{
    public Task CreateAsync(CreateInvitationArgs args)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        string? tenantId,
        AccountLogged accountLogged)
    {
        AssertCanFilterInvitationsByCreatorOrSetDefault(
            ref creatorId,
            accountLogged);

        AssertCanFilterInvitationsByAppIdOrSetDefault(
            ref appId,
            accountLogged);

        AssertCanFilterInvitationsByTenantIdOrSetDefault(
            ref tenantId,
            accountLogged);

        var invitations = await invitationRepository
            .GetAllAsync(
            creatorId,
            appId,
            tenantId,
            accountLogged)
            .ConfigureAwait(false);

        return invitations;
    }

    private static void AssertCanFilterInvitationsByCreatorOrSetDefault(
        ref string? creatorId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(creatorId) &&
            accountLogged.Id != creatorId &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadInvitationsOfTenant);
        }

        if (Guard.IsNullOrEmpty(creatorId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadInvitationsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            creatorId = accountLogged.Id;
        }
    }

    private static void AssertCanFilterInvitationsByAppIdOrSetDefault(
        ref string? appId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(appId) &&
            !accountLogged.AppsIds.Contains(appId!) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadInvitationsOfTenant);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadInvitationsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }
    }

    private static void AssertCanFilterInvitationsByTenantIdOrSetDefault(
        ref string? tenantId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(tenantId) &&
            accountLogged.Tenant.Id != tenantId)
        {
            accountLogged.AssertPermission(PermissionKey.FullAccess);
        }

        if (Guard.IsNullOrEmpty(tenantId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadInvitationsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            tenantId = accountLogged.Tenant.Id;
        }
    }
}
