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
        string? tenantId,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged
            .Apps
            .ConvertAll(i => i.Id);

        var canSeeOfTenant = accountLogged.HasPermission(PermissionKey.GetAllInvitationsOfTenant);

        var query = _dbSet
            .Include(i => i.Creator)
            .Where(i => tenantId == null || i.TenantId == tenantId)
            .Where(i => (appId != null && i.AppId == appId) ||
            (appId == null && (tenantId == null || canSeeOfTenant || appsIdsOfAccountLogged.Contains(i.AppId))))
            .Where(i => creatorId == null || i.CreatorId == creatorId)
            ;

        var invitations = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Invitation>>(invitations);
    }
}
