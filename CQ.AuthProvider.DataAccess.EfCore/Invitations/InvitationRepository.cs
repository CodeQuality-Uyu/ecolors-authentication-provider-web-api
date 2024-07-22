using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.UnitOfWork.EfCore.Core;
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
        bool viewAll,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged
            .Apps
            .ConvertAll(i => i.Id);

        var canSeeOfTenant = accountLogged.HasPermission(PermissionKey.GetAllInvitationsFromTenant);

        var query = _dbSet
            .Include(i => i.Creator)
            .Where(i => viewAll || i.TenantId == accountLogged.Tenant.Id)
            .Where(i => creatorId == null || i.CreatorId == creatorId)
            .Where(i => (appId != null && i.AppId == appId) ||
            (appId == null && (viewAll || canSeeOfTenant || appsIdsOfAccountLogged.Contains(i.AppId))))
            ;

        var invitations = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Invitation>>(invitations);
    }
}
