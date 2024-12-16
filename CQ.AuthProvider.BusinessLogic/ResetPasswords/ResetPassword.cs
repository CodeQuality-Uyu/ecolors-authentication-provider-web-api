using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

public sealed record class ResetPassword()
{
    public const int TOLERANCE_IN_MINUTES = 15;

    public Guid Id { get; init; } = Guid.NewGuid();

    public Account Account { get; init; } = null!;

    public int Code { get; init; } = NewCode();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(TOLERANCE_IN_MINUTES);

    public static ResetPassword New(Account account) => new()
    {
        Account = account
    };

    public static int NewCode()
    {
        return new Random()
            .Next(100000, 999999);
    }
}
