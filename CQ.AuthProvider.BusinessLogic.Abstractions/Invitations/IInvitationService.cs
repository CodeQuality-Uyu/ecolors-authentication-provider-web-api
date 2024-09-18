using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public interface IInvitationService
{
    Task CreateAsync(
        CreateInvitationArgs args,
        AccountLogged accountLogged);

    Task<Pagination<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<CreateAccountResult> AcceptByIdAsync(
        string id,
        AcceptInvitationArgs args);

    Task DeclainByIdAsync(
        string id,
        string email);
}
