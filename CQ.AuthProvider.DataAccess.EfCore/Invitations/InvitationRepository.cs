using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

internal sealed class InvitationRepository(
    AuthDbContext context,
    IMapper mapper)
    : EfCoreRepository<InvitationEfCore>(context),
    IInvitationRepository
{
    public async Task<List<Invitation>> GetAllAsync(
        string? creatorId,
        string? appId,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged.AppsIds;

        var query = _entities
            .Include(i => i.Creator)
            .Where(i => i.TenantId == accountLogged.Tenant.Id)
            .Where(i => creatorId == null || i.CreatorId == creatorId)
            .Where(i =>
            appId == null ||
            (appId != null && i.AppId == appId) ||
            appsIdsOfAccountLogged.Contains(i.AppId))
            .AsNoTracking();

        var invitations = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Invitation>>(invitations);
    }

    public async Task CreateAndSaveAsync(Invitation invitation)
    {
        var invitationEfCore = mapper.Map<InvitationEfCore>(invitation);

        await CreateAsync(invitationEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistPendingByEmailAsync(string email)
    {
        var query = _entities
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
            await _baseContext.SaveChangesAsync().ConfigureAwait(false);

            return false;
        }

        return invitation.IsPending();
    }

    public async Task<Invitation> GetPendingByIdAsync(string id)
    {
        var query = _entities
            .Where(i => i.Id == id)
            .Where(i => DateTime.UtcNow <= i.ExpiresAt)
            .AsNoTracking();

        var invitation = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(invitation, id, nameof(id));

        return mapper.Map<Invitation>(invitation);
    }

    public async Task DeleteAndSaveByIdAsync(string id)
    {
        await DeleteAndSaveAsync(i => i.Id == id).ConfigureAwait(false);
    }
}
