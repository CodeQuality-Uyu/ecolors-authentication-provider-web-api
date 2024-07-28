
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public interface IInvitationService
{
    Task CreateAsync(CreateInvitationArgs args);

    Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        string? tenantId,
        AccountLogged accountLogged);
}
