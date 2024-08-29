using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public interface IInvitationRepository
{
    Task CreateAndSaveAsync(Invitation invitation);

    Task<bool> ExistPendingByEmailAsync(string email);

    Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        string? tenantId,
        AccountLogged accountLogged);

    Task<Invitation> GetPendingByIdAsync(string id);

    Task DeleteAndSaveByIdAsync(string id);
}
