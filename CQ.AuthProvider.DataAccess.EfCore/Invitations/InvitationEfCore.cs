
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

public sealed record class InvitationEfCore
{
    public string Id { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string CreatorId { get; init; } = null!;

    public AccountEfCore Creator { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
