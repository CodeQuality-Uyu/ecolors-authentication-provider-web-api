
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public interface IInvitationService
{
    Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged);

    Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        string? tenantId,
        AccountLogged accountLogged);

    Task<CreateAccountResult> AcceptByIdAsync(
        string id,
        AcceptInvitationArgs args);

    Task DeclainByIdAsync(
        string id,
        string email);
}
