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
        bool viewAll,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(creatorId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllInvitationsOfCreators);
        }

        if (Guard.IsNullOrEmpty(creatorId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsOfCreators) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsFromAppsOfAccountLogged) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            creatorId = accountLogged.Id;
        }

        if (Guard.IsNotNullOrEmpty(appId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllInvitationsFromAppsOfAccountLogged);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationsFromAppsOfAccountLogged) &&
            !accountLogged.HasPermission(PermissionKey.GetAllInvitationOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }

        if (viewAll)
        {
            accountLogged.AssertPermission(PermissionKey.FullAccess);
        }

        var invitations = await invitationRepository
            .GetAllAsync(
            creatorId,
            appId,
            viewAll,
            accountLogged)
            .ConfigureAwait(false);

        return invitations;
    }
}
