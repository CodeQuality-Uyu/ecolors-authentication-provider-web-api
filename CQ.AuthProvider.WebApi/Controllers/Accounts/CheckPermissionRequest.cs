using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers
{ 
    public sealed record class CheckPermissionRequest : Request<PermissionKey>
    {
        public string? Permission { get; init; }

        protected override PermissionKey InnerMap()
        {
            Guard.ThrowIsNullOrEmpty(this.Permission, nameof(this.Permission));
            
            var permission = Guard.Encode(this.Permission.Trim());

            Guard.ThrowIsNullOrEmpty(permission, nameof(this.Permission));

            return new PermissionKey(permission);
        }
    }
}
