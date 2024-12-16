using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

public sealed record class InvitationEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; init; } = null!;

    public int Code { get; init; }

    public Guid CreatorId { get; init; }

    public AccountEfCore Creator { get; init; } = null!;

    public Guid RoleId { get; init; }

    public RoleEfCore Role { get; init; } = null!;

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public Guid TenantId { get; init; }

    public TenantEfCore Tenant { get; init; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(Invitation.EXPIRATION_MINUTES);

    public bool IsPending()
    {
        return ExpiresAt <= DateTime.UtcNow;
    }

    public bool IsExpired()
    {
        return ExpiresAt > DateTime.UtcNow;
    }
}
