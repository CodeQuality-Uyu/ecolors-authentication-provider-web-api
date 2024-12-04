using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Emails;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

internal sealed class InvitationService(
    IInvitationRepository _invitationRepository,
    IRoleRepository _roleRepository,
    IEmailService _emailService,
    IIdentityRepository _identityRepository,
    IAccountInternalService _accountService,
    IUnitOfWork _unitOfWork)
    : IInvitationService
{

    public async Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged)
    {
        var app = accountLogged.Apps.Find(a => a.Id == args.AppId);
        if (args.AppId.HasValue && Guard.IsNull(app))
        {
            throw new InvalidOperationException($"App is not allowed");
        }
        app ??= accountLogged.AppLogged;

        var role = await _roleRepository
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
                CreatorName = accountLogged.FullName,
                invitation.Code
            });

        await _invitationRepository
            .CreateAndSaveAsync(invitation)
            .ConfigureAwait(false);
    }

    public async Task<Pagination<Invitation>> GetAllAsync(
        Guid? creatorId,
        Guid? appId,
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
        Guid id,
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

        var firstName = Guard.Normalize(args.FirstName);
        var lastName = Guard.Normalize(args.LastName);
        var account = new Account
        {
            Email = args.Email,
            FirstName = firstName,
            LastName = lastName,
            FullName = $"{firstName} {lastName}",
            ProfilePictureId = args.ProfilePictureId,
            Locale = args.Locale,
            TimeZone = args.TimeZone,
            Roles = [invitation.Role],
            Tenant = invitation.App.Tenant,
            Apps = [invitation.App]
        };

        var result = await _accountService
                .CreateIdentityAndSaveAsync(account, args.Password)
                .ConfigureAwait(false);

        try
        {
            await _unitOfWork.CommitChangesAsync();
        }
        catch (Exception)
        {
            await _identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);
        }

        return result;
    }

    public async Task DeclainByIdAsync(
        Guid id,
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
