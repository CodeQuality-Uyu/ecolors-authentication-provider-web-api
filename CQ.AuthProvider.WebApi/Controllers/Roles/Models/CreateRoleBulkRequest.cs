using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Roles.Models;

public sealed record class CreateRoleBulkRequest : Request<List<CreateRoleArgs>>
{
    public List<CreateRoleRequest>? Roles { get; init; }

    protected override List<CreateRoleArgs> InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Roles, nameof(Roles));

        return Roles.ConvertAll(r => r.Map());
    }
}
