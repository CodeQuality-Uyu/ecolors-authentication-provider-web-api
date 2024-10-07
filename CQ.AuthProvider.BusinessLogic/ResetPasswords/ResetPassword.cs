using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public sealed record class ResetPassword()
{
    public const int TOLERANCE_IN_MINUTES = 15;

    public string Id { get; init; } = Db.NewId();

    public Account Account { get; init; } = null!;

    public string Code { get; init; } = NewCode();

    public DateTimeOffset CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(TOLERANCE_IN_MINUTES);

    public ResetPassword(Account account)
        : this()
    {
        Account = account;
    }

    public static string NewCode()
    {
        return new Random()
            .Next(100000, 999999)
            .ToString();
    }
}
