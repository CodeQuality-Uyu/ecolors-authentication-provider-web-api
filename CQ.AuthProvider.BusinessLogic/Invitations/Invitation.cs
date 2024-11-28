using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

public sealed record class Invitation()
{
    public const int EXPIRATION_MINUTES = 15;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; init; } = null!;

    public string Code { get; init; } = null!;

    public Account Creator { get; init; } = null!;

    public Role Role { get; init; } = null!;

    public App App { get; init; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

    // For new Invitation
    public Invitation(
        string email,
        Role role,
        App app,
        Account creator)
        : this()
    {
        Email = email;
        Role = role;
        App = app;
        Creator = creator;
        Code = ResetPassword.NewCode();
    }
}
