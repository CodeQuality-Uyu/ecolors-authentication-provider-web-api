using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;
using AutoMapper;
using CQ.Utility;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations;

[ApiController]
[Route("permissions")]
[CQAuthorization]
[ValidateAccount]
public class PermissionController(
    IMapper mapper,
    IPermissionService permissionService)
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreatePermissionRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await permissionService
            .CreateAsync(args)
            .ConfigureAwait(false);
    }

    [HttpPost("bulk")]
    public async Task CreateBulkAsync(CreatePermissionBulkRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await permissionService
            .CreateBulkAsync(args)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<PermissionBasicInfoResponse>> GetAllAsync(
        [FromQuery] bool isPrivate = false,
        [FromQuery] string? roleId = null)
    {
        var accountLogged = this.GetAccountLogged();

        var permissions = await permissionService
            .GetAllAsync(isPrivate, roleId, accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<List<PermissionBasicInfoResponse>>(permissions);
    }
}
