using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public sealed record class Invitation()
{
    public const int EXPIRATION_MINUTES = 15;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; init; } = null!;

    public int Code { get; init; } = ResetPassword.NewCode();

    public Account Creator { get; init; } = null!;

    public Role Role { get; init; } = null!;

    public App App { get; init; } = null!;

    public Tenant Tenant { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMinutes(EXPIRATION_MINUTES);

    public static Invitation New(
        string email,
        Role role,
        App app,
        Account creator) => new()
        {
            Email = email,
            Role = role,
            App = app,
            Creator = creator,
            Tenant = creator.Tenant
        };
}
