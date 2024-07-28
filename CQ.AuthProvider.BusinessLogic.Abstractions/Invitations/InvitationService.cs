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
        if (Guard.IsNotNullOrEmpty(creatorId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllInvitationsOfCreator);
        }

        if (Guard.IsNullOrEmpty(creatorId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsOfCreator) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            creatorId = accountLogged.Id;
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsOfAppsOfAccountLogged) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }

        if (Guard.IsNotNullOrEmpty(appId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllInvitationsOfAppsOfAccountLogged);
        }

        if (Guard.IsNullOrEmpty(tenantId) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            tenantId = accountLogged.Tenant.Id;
        }

        if (Guard.IsNotNullOrEmpty(tenantId))
        {
            accountLogged.AssertPermission(PermissionKey.FullAccess);
        }

        var invitations = await invitationRepository
            .GetAllAsync(
            creatorId,
            appId,
            tenantId,
            accountLogged)
            .ConfigureAwait(false);

        return invitations;
    }
}
