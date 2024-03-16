using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
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
