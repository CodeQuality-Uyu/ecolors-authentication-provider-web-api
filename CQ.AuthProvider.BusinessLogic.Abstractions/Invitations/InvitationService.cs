using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Emails;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

internal sealed class InvitationService(
    IInvitationRepository _invitationRepository,
    IRoleInternalService _roleService,
    IEmailService _emailService,
    IAccountInternalService _accountService,
    IUnitOfWork _unitOfWork)
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

        var role = await _roleService
            .GetByIdAsync(args.RoleId)
            .ConfigureAwait(false);
        if (role.AppId != app.Id)
        {
            throw new InvalidOperationException($"Role not in app");
        }

        var existPendingInvitation = await _invitationRepository
            .ExistPendingByEmailAsync(args.Email)
            .ConfigureAwait(false);
        if (existPendingInvitation)
        {
            throw new InvalidOperationException($"Exist a pending invitation for the email");
        }

        await _accountService
            .AssertByEmailAsync(args.Email)
            .ConfigureAwait(false);

        var invitation = new Invitation(
            args.Email,
            role,
            app,
            accountLogged);

        await _emailService.SendAsync(
            args.Email,
            EmailTemplateKey.InviteUser,
            new
            {
                invitation.Code
            });

        await _invitationRepository
            .CreateAndSaveAsync(invitation)
            .ConfigureAwait(false);
    }

    public async Task<Pagination<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var invitations = await _invitationRepository
            .GetAllAsync(
            creatorId,
            appId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return invitations;
    }

    public async Task<CreateAccountResult> AcceptByIdAsync(
        string id,
        AcceptInvitationArgs args)
    {
        var invitation = await _invitationRepository
            .GetPendingByIdAsync(id)
            .ConfigureAwait(false);

        if (invitation.Code != args.Code ||
            invitation.Email != args.Email)
        {
            throw new InvalidOperationException("Invalid email or code");
        }

        await _invitationRepository
            .DeleteByIdAsync(id)
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

        var result = await _accountService
            .CreateAsync(account)
            .ConfigureAwait(false);

        await _unitOfWork.CommitChangesAsync();

        return result;
    }

    public async Task DeclainByIdAsync(
        string id,
        string email)
    {
        var invitation = await _invitationRepository
            .GetPendingByIdAsync(id)
            .ConfigureAwait(false);

        if (invitation.Email != email)
        {
            throw new InvalidOperationException("Invalid invitation");
        }

        await _invitationRepository
            .DeleteAndSaveByIdAsync(id)
            .ConfigureAwait(false);
    }
}
