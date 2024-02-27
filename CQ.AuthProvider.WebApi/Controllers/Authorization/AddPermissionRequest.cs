using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class AddPermissionRequest : Request<AddPermission>
    {
        public List<string>? PermissionsKeys { get; init; }

        protected override AddPermission InnerMap()
        {
            return new AddPermission(this.PermissionsKeys ?? new List<string>());
        }
    }
}
