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

    public string Code { get; init; } = null!;

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

    private InvitationEfCore(
        string email,
        string code,
        Guid roleId,
        Guid appId,
        Guid creatorId,
        Guid tenantId)
        : this()
    {
        Email = email;
        Code = code;
        RoleId = roleId;
        AppId = appId;
        CreatorId = creatorId;
        TenantId = tenantId;
    }

    internal InvitationEfCore(Invitation invitation)
        : this(invitation.Email,
              invitation.Code,
              invitation.Role.Id,
              invitation.App.Id,
              invitation.Creator.Id,
              invitation.Creator.Tenant.Id)
    {
        Id = invitation.Id;
        CreatedAt = invitation.CreatedAt;
        ExpiresAt = invitation.ExpiresAt;
    }

    public bool IsPending()
    {
        return ExpiresAt <= DateTime.UtcNow;
    }

    public bool IsExpired()
    {
        return ExpiresAt > DateTime.UtcNow;
    }
}
