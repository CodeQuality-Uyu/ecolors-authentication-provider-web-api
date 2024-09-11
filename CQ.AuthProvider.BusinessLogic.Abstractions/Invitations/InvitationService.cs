using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Emails;
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
        AccountLogged accountLogged)
    {
        var invitations = await invitationRepository
            .GetAllAsync(
            creatorId,
            appId,
            accountLogged)
            .ConfigureAwait(false);

        return invitations;
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
