using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public interface IInvitationService
{
    Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged);

    Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        AccountLogged accountLogged);

    Task<CreateAccountResult> AcceptByIdAsync(
        string id,
        AcceptInvitationArgs args);

    Task DeclainByIdAsync(
        string id,
        string email);
}
