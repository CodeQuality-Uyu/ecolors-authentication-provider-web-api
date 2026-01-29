using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Subscriptions;

public sealed record Subscription
{
    public Guid Id { get; init; }

    public string Value { get; init; } = null!;

    public DateTimeOffset CreatedAtUtc { get; init; }

    public App App { get; init; } = null!;

    public List<Permission> Permissions { get; init; } = []; 
}