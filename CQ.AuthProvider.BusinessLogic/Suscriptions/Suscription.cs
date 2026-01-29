using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Suscriptions;

public sealed record Suscription
{
    public Guid Id { get; init; }

    public string Value { get; init; } = null!;

    public DateTimeOffset CreatedAtUtc { get; init; }

    public App App { get; init; } = null!; 
}