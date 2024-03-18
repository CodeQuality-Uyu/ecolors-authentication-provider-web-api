using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class PermissionResponse : Response<Permission>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Key { get; init; }

        public bool IsPublic { get; init; }

        public PermissionResponse(Permission entity) : base(entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.IsPublic = entity.IsPublic;
            this.Key = entity.Key.ToString();
        }
    }
}
