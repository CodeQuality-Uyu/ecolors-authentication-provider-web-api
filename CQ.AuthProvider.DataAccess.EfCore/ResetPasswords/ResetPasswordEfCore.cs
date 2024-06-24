using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;

public sealed record class ResetPasswordEfCore
{
    public string Id { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public string Code { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public ResetPasswordStatus Status { get; set; } = ResetPasswordStatus.Pending;

    /// <summary>
    /// For EfCore
    /// </summary>
    public ResetPasswordEfCore()
    {
    }

    /// <summary>
    /// For new ResetPassword
    /// </summary>
    /// <param name="id"></param>
    /// <param name="code"></param>
    /// <param name="accountId"></param>
    public ResetPasswordEfCore(
        string id,
        string code,
        string accountId)
    {
        Id = id;
        Code = code;
        AccountId = accountId;
    }
}
