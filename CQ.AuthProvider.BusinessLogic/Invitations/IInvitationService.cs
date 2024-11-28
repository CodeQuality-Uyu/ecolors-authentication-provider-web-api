using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public interface IInvitationService
{
    Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged);

    Task<Pagination<Invitation>> GetAllAsync(
        Guid? creatorId,
        Guid? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<CreateAccountResult> AcceptByIdAsync(
        Guid id,
        AcceptInvitationArgs args);

    Task DeclainByIdAsync(
        Guid id,
        string email);
}
