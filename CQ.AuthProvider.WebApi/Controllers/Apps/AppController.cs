using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

[ApiController]
[Route("apps")]
[BearerAuthentication]
public sealed class AppController(
    IAppService _appService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpPost]
    [BearerAuthentication]
    public async Task<AppCreatedResponse> CreateAsync(CreateAppArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        var appCreated = await _appService
            .CreateAsync(request, accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<AppCreatedResponse>(appCreated);
    }

    [HttpGet]
    [BearerAuthentication]
    public async Task<Pagination<AppBasicInfoResponse>> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var apps = await _appService
            .GetAllAsync(page, pageSize, accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<AppBasicInfoResponse>>(apps);
    }
}
