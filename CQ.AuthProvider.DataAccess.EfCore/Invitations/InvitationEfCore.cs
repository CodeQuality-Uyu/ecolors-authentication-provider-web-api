using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

public sealed record class InvitationEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string Code { get; init; } = null!;

    public string CreatorId { get; init; } = null!;

    public AccountEfCore Creator { get; init; } = null!;

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(Invitation.EXPIRATION_MINUTES);

    private InvitationEfCore(
        string email,
        string code,
        string roleId,
        string appId,
        string creatorId,
        string tenantId)
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
              invitation.Creator.TenantValue.Id)
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
