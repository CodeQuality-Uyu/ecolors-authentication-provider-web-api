using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Emails;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

internal sealed class InvitationService(
    IInvitationRepository invitationRepository,
    IRoleInternalService roleService,
    IEmailService emailService,
    IAccountInternalService accountService)
    : IInvitationService
{
    public async Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged)
    {
        var app = accountLogged.Apps.Find(a => a.Id == args.AppId);
        if (Guard.IsNotNullOrEmpty(args.AppId) && Guard.IsNull(app))
        {
            throw new InvalidOperationException($"App is not allowed");
        }
        app ??= accountLogged.AppLogged;

        var role = await roleService
            .GetByIdAsync(args.RoleId)
            .ConfigureAwait(false);
        if (role.App.Id != app.Id)
        {
            throw new InvalidOperationException($"Role not in app");
        }

        var existPendingInvitation = await invitationRepository
            .ExistPendingByEmailAsync(args.Email)
            .ConfigureAwait(false);
        if (existPendingInvitation)
        {
            throw new InvalidOperationException($"Exist a pending invitation for the email");
        }

        var invitation = new Invitation(
            args.Email,
            role,
            app,
            accountLogged);

        await emailService.SendAsync(
            args.Email,
            EmailTemplateKey.InviteUser,
            new
            {
                invitation.Code
            });

        await invitationRepository
            .CreateAndSaveAsync(invitation)
            .ConfigureAwait(false);
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

    public async Task<CreateAccountResult> AcceptByIdAsync(
        string id,
        AcceptInvitationArgs args)
    {
        var invitation = await invitationRepository
            .GetPendingByIdAsync(id)
            .ConfigureAwait(false);

        if (invitation.Code != args.Code &&
            invitation.Email != args.Email)
        {
            throw new InvalidOperationException("Invalid email or code");
        }

        await invitationRepository
            .DeleteAndSaveByIdAsync(id)
            .ConfigureAwait(false);

        var account = new CreateAccountArgs(
            args.Email,
            args.Password,
            args.FirstName,
            args.LastName,
            args.Locale,
            args.TimeZone,
            invitation.Role.Id,
            args.ProfilePictureId,
            invitation.App.Id);

        var result = await accountService
            .InternalCreationAsync(account)
            .ConfigureAwait(false);

        return result;
    }

    public async Task DeclainByIdAsync(
        string id,
        string email)
    {
        var invitation = await invitationRepository
            .GetPendingByIdAsync(id)
            .ConfigureAwait(false);

        if (invitation.Email != email)
        {
            throw new InvalidOperationException("Invalid invitation");
        }

        await invitationRepository
            .DeleteAndSaveByIdAsync(id)
            .ConfigureAwait(false);
    }
}
