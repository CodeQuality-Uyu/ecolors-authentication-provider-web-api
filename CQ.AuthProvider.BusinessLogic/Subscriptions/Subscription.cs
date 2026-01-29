using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Subscriptions;

public sealed record Subscription
{
    public Guid Id { get; init; }

    public string Value { get; init; } = null!;

    public DateTimeOffset CreatedAtUtc { get; init; }

    public App App { get; init; } = null!; 
}