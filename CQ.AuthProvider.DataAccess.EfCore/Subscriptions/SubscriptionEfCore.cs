using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;

namespace CQ.AuthProvider.DataAccess.EfCore.Subscriptions;

public sealed record SubscriptionEfCore
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Value { get; init; } = Guid.NewGuid().ToString("N");

    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public IList<PermissionEfCore> Permissions { get; init; } = [];
}