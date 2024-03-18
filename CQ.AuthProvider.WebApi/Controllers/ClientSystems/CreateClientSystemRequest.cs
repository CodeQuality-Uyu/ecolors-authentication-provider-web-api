using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.ClientSystems
{
    public sealed record class CreateClientSystemRequest : Request<string>
    {
        public string? Name { get; init; }

        public CreateClientSystemRequest(Request<string> original) : base(original)
        {
        }

        protected override string InnerMap()
        {
            return Guard.Encode(this.Name ?? string.Empty, nameof(this.Name));
        }
    }
}
