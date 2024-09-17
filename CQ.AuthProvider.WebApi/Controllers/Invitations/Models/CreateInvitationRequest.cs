using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

public sealed record CreateInvitationRequest
    : Request<CreateInvitationArgs>
{
    public string? Email { get; init; }

    public string? RoleId { get; init; }

    public string? AppId { get; init; }

    protected override CreateInvitationArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Email, nameof(Email));
        Guard.ThrowIsNullOrEmpty(RoleId, nameof(RoleId));

        return new CreateInvitationArgs(
            Email!,
            RoleId!,
            AppId);
    }
}
