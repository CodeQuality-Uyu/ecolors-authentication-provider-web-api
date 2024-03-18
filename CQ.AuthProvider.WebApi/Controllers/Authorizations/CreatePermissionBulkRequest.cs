using CQ.ApiElements.Dtos;
using CQ.ApiElements.Extensions;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class CreatePermissionBulkRequest : Request<List<CreatePermission>>
    {
        public List<CreatePermissionRequest>? Permissions { get; init; }

        protected override List<CreatePermission> InnerMap()
        {
            Guard.ThrowIsNullOrEmpty(this.Permissions, nameof(this.Permissions));

            return this.Permissions.Select(p => p.Map()).ToList();
        }
    }
}
