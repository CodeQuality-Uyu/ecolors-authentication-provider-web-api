using CQ.ApiElements.Dtos;

namespace CQ.AuthProvider.WebApi.Controllers
{ 
    public sealed record class CheckPermissionRequest
    {
        public string? Permission { get; init; }
    }
}
