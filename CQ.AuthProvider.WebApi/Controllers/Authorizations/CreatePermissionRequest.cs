using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class CreatePermissionRequest : Request<CreatePermission>
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public string? Key { get; init; }

        public bool IsPublic { get; init; }

        protected override CreatePermission InnerMap()
        {
            return new CreatePermission(
                Name ?? string.Empty,
                Description ?? string.Empty,
                Key ?? string.Empty,
                IsPublic);
        }
    }
}
