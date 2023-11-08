using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class MiniPermissionResponse : Response<MiniPermission>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Key { get; private set; }

        public MiniPermissionResponse(MiniPermission entity) : base(entity) { }

        protected override void Map(MiniPermission entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Key = entity.Key;
        }
    }
}
