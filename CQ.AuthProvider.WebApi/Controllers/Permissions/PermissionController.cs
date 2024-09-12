using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;
using AutoMapper;
using CQ.Utility;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions;

[ApiController]
[Route("permissions")]
[CQAuthentication]
[SecureAuthorization]
public class PermissionController(
    IMapper mapper,
    IPermissionService permissionService
    )
    : ControllerBase
{

    [HttpPost]
    public async Task CreateAsync(CreatePermissionRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await permissionService
            .CreateAsync(
            args,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPost("bulk")]
    public async Task CreateBulkAsync(CreatePermissionBulkRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await permissionService
            .CreateBulkAsync(
            args,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<Pagination<PermissionBasicInfoResponse>> GetAllAsync(
        [FromQuery] string? appId,
        [FromQuery] bool? isPrivate,
        [FromQuery] string? roleId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var permissions = await permissionService
            .GetAllAsync(
            appId,
            isPrivate,
            roleId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<PermissionBasicInfoResponse>>(permissions);
    }
}
