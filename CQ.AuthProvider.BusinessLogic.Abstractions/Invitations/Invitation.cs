using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public sealed record class Invitation()
{
    public const int EXPIRATION_MINUTES = 15;

    public string Id { get; init; } = Db.NewId();

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
