using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class AddPermissionRequest : Request<AddPermission>
    {
        public IList<string>? PermissionsKeys { get; init; }

        protected override AddPermission InnerMap()
        {
            return new AddPermission(this.PermissionsKeys ?? new List<string>());
        }
    }
}
