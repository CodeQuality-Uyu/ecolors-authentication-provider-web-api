using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public interface IInvitationRepository
{
    Task CreateAndSaveAsync(Invitation invitation);

    Task<bool> ExistPendingByEmailAsync(string email);

    Task<Pagination<Invitation>> GetAllAsync(
        Guid? creatorId,
        Guid? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<Invitation> GetPendingByIdAsync(Guid id);

    Task DeleteByIdAsync(Guid id);

    Task DeleteAndSaveByIdAsync(Guid id);
}
