using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.ClientSystems;

namespace CQ.AuthProvider.WebApi.Controllers.ClientSystems
{
    public sealed record class ClientSystemResponse : Response<ClientSystem>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Role { get; init; }

        public List<string> Permissions { get; init; }

        public ClientSystemResponse(ClientSystem entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Role = entity.Role.ToString();
            Permissions = entity.Permissions.Select(p => p.ToString()).ToList();
        }
    }
}
