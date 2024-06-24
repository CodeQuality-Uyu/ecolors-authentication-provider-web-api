using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.ResetPasswords.Models;

public sealed record class ResetPasswordAcceptedRequest : Request<AcceptResetPasswordArgs>
{
    public string? NewPassword { get; init; }

    public string? Code { get; init; }

    protected override AcceptResetPasswordArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(NewPassword, nameof(NewPassword));
        Guard.ThrowIsNullOrEmpty(Code, nameof(Code));

        return new AcceptResetPasswordArgs(
            NewPassword!,
            Code!);
    }
}
