using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;

public sealed record class ResetPassword
{
    public const int TOLERANCE_IN_MINUTES = 15;

    public string Id { get; init; } = Db.NewId();

    public Account Account { get; init; } = null!;

    public string Code { get; init; } = NewCode();

    public DateTimeOffset CreatedAt { get; init; }

    public ResetPasswordStatus Status { get; init; }

    /// <summary>
    /// For mapping
    /// </summary>
    public ResetPassword()
    {
    }

    public ResetPassword(Account account)
    {
        Account = account;
    }

    public bool HasExpired()
    {
        return CreatedAt.AddMinutes(TOLERANCE_IN_MINUTES) > DateTimeOffset.UtcNow;
    }

    public static string NewCode()
    {
        return new Random()
            .Next(1111, 9999)
            .ToString();
    }
}
