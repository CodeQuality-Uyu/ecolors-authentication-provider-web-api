using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateSessionResponse : Response<SessionCreated>
    {
        public string AuthId { get; private set; }

        public string Email { get; private set; }

        public string Token { get; private set; }

        public IList<string> Roles { get; private set; }
        
        public CreateSessionResponse(SessionCreated session) : base(session) { }

        protected override void Map(SessionCreated entity)
        {
            this.AuthId = entity.AuthId;
            this.Email = entity.Email;
            this.Token = entity.Token;
            this.Roles = entity.Roles;
        }
    }
}
