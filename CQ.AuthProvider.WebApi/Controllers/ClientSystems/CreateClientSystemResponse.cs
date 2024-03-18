using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.ClientSystems;

namespace CQ.AuthProvider.WebApi.Controllers.ClientSystems
{
    public sealed record class CreateClientSystemResponse : Response<string>
    {
        public string PrivateKey { get; init; }

        public CreateClientSystemResponse(string entity) : base(entity)
        {
            this.PrivateKey = entity;
        }
    }
}
