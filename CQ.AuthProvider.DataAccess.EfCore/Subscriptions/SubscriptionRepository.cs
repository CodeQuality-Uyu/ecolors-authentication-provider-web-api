using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Subscriptions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Subscriptions;

internal sealed class SubscriptionRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper) 
    : EfCoreRepository<SubscriptionEfCore>(context), ISubscriptionRepository
{
    public async Task<Subscription> CreateAsync(App app)
    {
        var subscriptionEfCore = new SubscriptionEfCore
        {
            AppId = app.Id,
        };

        await CreateAsync(subscriptionEfCore).ConfigureAwait(false);

        IList<SubscriptionPermission> permissions = [
            new SubscriptionPermission
            {
                SubscriptionId = subscriptionEfCore.Id,
                PermissionId = AuthConstants.CREATE_CLIENT_APP_PERMISSION_ID,
            },
            new SubscriptionPermission
            {
                SubscriptionId = subscriptionEfCore.Id,
                PermissionId = AuthConstants.CREATE_CREDENTIALS_FOR_PERMISSION_ID,
            }
        ];

        await context
            .SubscriptionPermissions
            .AddRangeAsync(permissions)
            .ConfigureAwait(false);

        var subscription = mapper.Map<Subscription>(subscriptionEfCore);
        
        return subscription;
    }

    public async Task<Subscription> GetByValueAsync(string value)
    {
        var subscriptionEfCore = await Entities
            .AsNoTracking()
            .Where(s => s.Value == value)
            .Include(s => s.App)
            .Include(s => s.Permissions)
            .AsSplitQuery()
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(subscriptionEfCore, value, nameof(Subscription.Value));

        var subscription = mapper.Map<Subscription>(subscriptionEfCore);

        return subscription; 
    }
}