
namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public sealed record AccountCreatedResponse(
    AccountLoggedResponse Account,
    string Token);
