using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;

public sealed record class ResetPasswordEfCore()
{
    public string Id { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(ResetPassword.TOLERANCE_IN_MINUTES);

    // For new ResetPassword
    public ResetPasswordEfCore(
        string id,
        string code,
        string accountId)
        : this()
    {
        Id = id;
        Code = code;
        AccountId = accountId;
    }
}
