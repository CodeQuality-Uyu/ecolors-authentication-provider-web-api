using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Me.Models;

public record UpdateTenantNameRequest
    : Request<string>
{
    public string? NewName { get; init; }

    protected override string InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(NewName, nameof(NewName));

        return NewName!;
    }
}
