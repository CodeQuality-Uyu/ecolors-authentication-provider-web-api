using CQ.AuthProvider.DataAccess.EfCore.Apps;

namespace CQ.AuthProvider.DataAccess.EfCore.Suscriptions;

public sealed record SuscriptionEfCore
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Value { get; init; } = Guid.NewGuid().ToString("N");

    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;
}