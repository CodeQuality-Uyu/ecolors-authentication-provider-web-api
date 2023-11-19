using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class RoleResponse : Response<Role>
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get;private set; }

        public string Key { get; private set; }

        public bool IsPublic { get; private set; }

        public RoleResponse(Role entity) : base(entity)
        {
        }

        protected override void Map(Role entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Key = entity.Key;
            this.IsPublic = entity.IsPublic;
        }
    }
}
