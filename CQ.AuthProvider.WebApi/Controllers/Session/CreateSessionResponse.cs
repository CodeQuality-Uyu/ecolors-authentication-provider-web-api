using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateSessionResponse : Response<Session>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
        
        public CreateSessionResponse(Session session) : base(session) { }

        protected override void Map(Session entity)
        {
            Id = entity.AuthId;
            Email = entity.Email;
            Token = entity.Token;
        }
    }
}
