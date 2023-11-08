using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateRoleRequest : Request<CreateRole>
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public string? Key { get; init; }

        public IList<string>? PermissionKeys { get; init; }

        public bool IsPublic { get; init; }

        protected override CreateRole InnerMap()
        {
            return new CreateRole(Name ?? string.Empty, Description ?? string.Empty, Key ?? string.Empty, PermissionKeys ?? new List<string>(), IsPublic);
        }
    }
}
