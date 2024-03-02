using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    public sealed record class MiniPermissionResponse : Response<MiniPublicPermission>
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Key { get; private set; }

        public MiniPermissionResponse(MiniPublicPermission entity) : base(entity) { }

        protected override void Map(MiniPublicPermission entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Key = entity.Key;
        }
    }
}
