
namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords.Exceptions;

public sealed class CodesNotMatchException(
    string code,
    string email)
    : Exception
{
    public string Code { get; init; } = code;

    public string Email { get; init; } = email;
}
