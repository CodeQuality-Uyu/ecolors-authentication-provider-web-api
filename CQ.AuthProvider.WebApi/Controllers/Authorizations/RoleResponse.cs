using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class RoleResponse : Response<Role>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Key { get; init; }

        public bool IsPublic { get; init; }

        public bool IsDefault { get; init; }

        public RoleResponse(Role entity) : base(entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Key = entity.Key.ToString();
            this.IsPublic = entity.IsPublic;
            this.IsDefault = entity.IsDefault;
        }
    }
}
