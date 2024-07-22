using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public interface IInvitationRepository
{
    Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        bool viewAll,
        AccountLogged accountLogged);
}
