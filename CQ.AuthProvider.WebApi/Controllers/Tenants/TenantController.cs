using CQ.AuthProvider.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

[ApiController]
[Route("tenants")]
[CQAuthorization]
public sealed class TenantController
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync()
    {
    }

    [HttpGet]
    public async Task GetAllAsync()
    {
    }

    [HttpGet("{id}")]
    public async Task GetByIdAsync()
    {
    }
}
