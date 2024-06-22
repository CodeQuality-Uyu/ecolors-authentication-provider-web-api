
namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public sealed record class AccountCreatedResponse
    : AccountLoggedResponse
{
    public string Token { get; init; } = null!;
}
