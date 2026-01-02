using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Extensions;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Roles;

[ApiController]
[Route("roles")]
[BearerAuthentication]
[SecureAuthorization]
public class RoleController(
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper mapper,
    IRoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreateRoleArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await roleService
            .CreateAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPost("bulk")]
    public async Task CreateBulkAsync(CreateBulkRoleArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await roleService
            .CreateBulkAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPost("{id}/permissions")]
    public async Task AddPermissionAsync(Guid id, AddPermissionArgs request)
    {
        await roleService
            .AddPermissionByIdAsync(
            id,
            request)
            .ConfigureAwait(false);
    }

    [HttpDelete("{id}/permissions/{permissionId}")]
    public async Task RemovePermissionAsync(Guid id, Guid permissionId)
    {
        await roleService
            .RemovePermissionByIdAsync(
            id,
            permissionId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<Pagination<RoleBasicInfoResponse>> GetAllAsync(
        [FromQuery] Guid? appId,
        [FromQuery] bool? isPrivate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var roles = await roleService
            .GetAllAsync(
            appId,
            isPrivate,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<RoleBasicInfoResponse>>(roles);
    }
}
