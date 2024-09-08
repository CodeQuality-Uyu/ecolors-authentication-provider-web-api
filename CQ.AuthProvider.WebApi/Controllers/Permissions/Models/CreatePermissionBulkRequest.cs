using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

public sealed record class CreatePermissionBulkRequest : Request<List<CreatePermissionArgs>>
{
    public List<CreatePermissionRequest>? Permissions { get; init; }

    protected override List<CreatePermissionArgs> InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Permissions, nameof(Permissions));

        return Permissions.ConvertAll(p => p.Map());
    }
}
