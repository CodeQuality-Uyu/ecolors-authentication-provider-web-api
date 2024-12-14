using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;

public sealed record class ResetPasswordEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid AccountId { get; init; }

    public AccountEfCore Account { get; init; } = null!;

    public int Code { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMinutes(ResetPassword.TOLERANCE_IN_MINUTES);

    // For new ResetPassword
    public ResetPasswordEfCore(
        Guid id,
        int code,
        Guid accountId)
        : this()
    {
        Id = id;
        Code = code;
        AccountId = accountId;
    }
}
