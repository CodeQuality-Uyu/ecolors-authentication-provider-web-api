using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.DataAccess.Mongo.Authorizations;

namespace CQ.AuthProvider.WebApi.Controllers.Roles.Models
{
    public sealed record class MiniPublicRoleResponse : Response<MiniRole>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Key { get; private set; }

        public MiniPublicRoleResponse(MiniRole entity) : base(entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Key = entity.Key;
        }
    }
}
