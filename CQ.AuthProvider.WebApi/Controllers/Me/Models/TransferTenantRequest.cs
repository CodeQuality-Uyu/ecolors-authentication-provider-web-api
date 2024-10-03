using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Me.Models;

public record TransferTenantRequest : Request<string>
{
    public string? NewOwnerId { get; init; }

    protected override string InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(NewOwnerId, nameof(NewOwnerId));

        return NewOwnerId!;
    }
}
