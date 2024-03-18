using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class CreateRoleBulkRequest : Request<List<CreateRole>>
    {
        public List<CreateRoleRequest>? Roles { get; init; }

        protected override List<CreateRole> InnerMap()
        {
            Guard.ThrowIsNullOrEmpty(this.Roles, nameof(this.Roles));

            return this.Roles.Select(r => r.Map()).ToList();
        }
    }
}
