using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

internal sealed class InvitationRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper)
    : EfCoreRepository<InvitationEfCore>(context),
    IInvitationRepository
{
    public async Task<Pagination<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged.AppsIds;

        var query = Entities
            .Where(i => i.TenantId == accountLogged.TenantValue.Id)
            .Where(i => creatorId == null || i.CreatorId == creatorId)
            .Where(i =>
            appId == null ||
            (appId != null && i.AppId == appId) ||
            appsIdsOfAccountLogged.Contains(i.AppId))
            .Paginate(page, pageSize)
            .AsNoTracking();

        var invitations = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Invitation>>(invitations);
    }

    async Task IInvitationRepository.CreateAndSaveAsync(Invitation invitation)
    {
        var invitationEfCore = mapper.Map<InvitationEfCore>(invitation);

        await CreateAndSaveAsync(invitationEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistPendingByEmailAsync(string email)
    {
        var query = Entities
            .Where(i => i.Email == email)
            .AsNoTracking();

        var invitation = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (Guard.IsNull(invitation))
        {
            return false;
        }

        if (invitation.IsExpired())
        {
            await DeleteAndSaveAsync(invitation).ConfigureAwait(false);
            await BaseContext.SaveChangesAsync().ConfigureAwait(false);

            return false;
        }

        return invitation.IsPending();
    }

    public async Task<Invitation> GetPendingByIdAsync(string id)
    {
        var query = Entities
            .Where(i => i.Id == id)
            .Where(i => DateTime.UtcNow <= i.ExpiresAt)
            .AsNoTracking();

        var invitation = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(invitation, id, nameof(id));

        return mapper.Map<Invitation>(invitation);
    }

    public Task DeleteByIdAsync(string id)
    {
        var query = Entities
            .Where(i => i.Id == id);

        Entities.RemoveRange(query);

        return Task.CompletedTask;
    }

    public async Task DeleteAndSaveByIdAsync(string id)
    {
        await DeleteAndSaveAsync(i => i.Id == id).ConfigureAwait(false);
    }
}
