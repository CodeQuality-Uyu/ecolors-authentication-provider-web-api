using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;
using AutoMapper;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.ApiElements.Filters.Authentications;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions;

[ApiController]
[Route("permissions")]
[BearerAuthentication]
[SecureAuthorization]
public class PermissionController(
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper mapper,
    IPermissionService permissionService)
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreatePermissionArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await permissionService
            .CreateAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPost("bulk")]
    public async Task CreateBulkAsync(CreateBulkPermissionArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await permissionService
            .CreateBulkAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<Pagination<PermissionBasicInfoResponse>> GetAllAsync(
        [FromQuery] Guid? appId,
        [FromQuery] bool? isPrivate,
        [FromQuery] Guid? roleId,
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
