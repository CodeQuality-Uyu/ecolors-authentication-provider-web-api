using CQ.ApiElements.Dtos;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions.Models
{
    public readonly struct TokenValidationResponse
    {
        public bool IsValid { get; init; }

        public TokenValidationResponse(bool entity)
        {
            IsValid = entity;
        }
    }
}
