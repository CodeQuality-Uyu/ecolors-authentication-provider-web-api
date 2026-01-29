using CQ.AuthProvider.DataAccess.EfCore.Permissions;

namespace CQ.AuthProvider.DataAccess.EfCore.Subscriptions;

public sealed record SubscriptionPermission
{
    public Guid SubscriptionId { get; init; }

    public SubscriptionEfCore Subscription { get; init; } = null!;

    public Guid PermissionId { get; init; }

    public PermissionEfCore Permission { get; init; } = null!;
}