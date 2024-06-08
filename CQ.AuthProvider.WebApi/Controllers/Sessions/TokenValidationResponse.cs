using CQ.ApiElements.Dtos;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    public readonly struct TokenValidationResponse
    {
        public bool IsValid { get; init; }

        public TokenValidationResponse(bool entity)
        {
            this.IsValid = entity;
        }
    }
}
