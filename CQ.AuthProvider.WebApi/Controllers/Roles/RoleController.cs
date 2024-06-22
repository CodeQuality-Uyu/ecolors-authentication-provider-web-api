using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Controllers.Roles.Models;
using AutoMapper;
using CQ.Utility;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Roles;

[ApiController]
[Route("roles")]
[CQAuthorization]
[ValidateAccount]
public class RoleController(
    IMapper mapper,
    IRoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreateRoleRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await roleService
            .CreateAsync(args)
            .ConfigureAwait(false);
    }

    [HttpPost("bulk")]
    public async Task CreateBulkAsync(CreateRoleBulkRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await roleService
            .CreateBulkAsync(args)
            .ConfigureAwait(false);
    }

    [HttpPost("{id}/permissions")]
    public async Task AddPermissionAsync(string id, AddPermissionRequest request)
    {
        Db.ThrowIsInvalidId(id, nameof(id));
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await roleService
            .AddPermissionByIdAsync(
            id,
            args)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<RoleBasicInfoResponse>> GetAllAsync(
        [FromQuery] bool isPrivate = false)
    {
        var accountLogged = this.GetAccountLogged()!;

        var roles = await roleService
            .GetAllAsync(
            isPrivate,
            accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<List<RoleBasicInfoResponse>>(roles);
    }
}
