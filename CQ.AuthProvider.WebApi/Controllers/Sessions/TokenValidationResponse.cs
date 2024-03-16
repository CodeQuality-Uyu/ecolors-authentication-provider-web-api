using CQ.ApiElements.Dtos;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    public sealed record class TokenValidationResponse : Response<bool>
    {
        public bool IsValid { get; init; }
        public TokenValidationResponse(bool entity) : base(entity)
        {
            this.IsValid = entity;
        }
    }
}
