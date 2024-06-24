
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public sealed record class AccountLogged : Account
{
    public string Token { get; init; } = null!;

    public AccountLogged(
        Account account,
        string token)
        : base(
            account.Email,
            account.FirstName,
            account.LastName,
            account.FullName,
            account.ProfilePictureUrl,
            account.Locale,
            account.TimeZone,
            account.Roles)
    {
        Id = account.Id;
        Token = token;
    }
}
