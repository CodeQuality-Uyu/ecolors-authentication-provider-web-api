using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public interface IInvitationRepository
{
    Task CreateAndSaveAsync(Invitation invitation);

    Task<bool> ExistPendingByEmailAsync(string email);

    Task<Pagination<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<Invitation> GetPendingByIdAsync(string id);

    Task DeleteByIdAsync(string id);

    Task DeleteAndSaveByIdAsync(string id);
}
