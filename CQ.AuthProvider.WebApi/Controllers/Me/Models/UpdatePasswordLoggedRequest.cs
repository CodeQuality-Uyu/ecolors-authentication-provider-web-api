using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Me.Models;

public sealed record UpdatePasswordLoggedRequest
    : Request<string>
{
    public string? NewPassword { get; init; }

    protected override string InnerMap()
    {
        Guard.ThrowIsInputInvalidPassword(NewPassword, nameof(NewPassword));

        return NewPassword!;
    }
}
