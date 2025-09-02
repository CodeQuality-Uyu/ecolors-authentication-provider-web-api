﻿using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

[ApiController]
[Route("apps")]
public sealed class AppController(
    IAppService _appService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpPost]
    [BearerAuthentication]
    [SecureAuthorization("create-app", "create-client")]
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
    [SecureAuthorization("getall-app", "getall-client")]
    public async Task<Pagination<AppBasicInfoResponse>> GetAllAsync(
        [FromQuery] Guid? fatherAppId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var apps = await _appService
            .GetPaginationAsync(
            fatherAppId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<AppBasicInfoResponse>>(apps);
    }

    [HttpGet("{id}")]
    public async Task<AppDetailInfoResponse> GetByIdAsync(Guid id)
    {
        var app = await _appService
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return _mapper.Map<AppDetailInfoResponse>(app);
    }

    [HttpPatch("{id}/colors")]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task UpdateColorsAsync(
        Guid id,
        CreateAppCoverBackgroundColorArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _appService
            .UpdateColorsByIdAsync(
            id,
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPatch("{id}/default-coin")]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task UpdateDefaultCoinAsync(
        Guid id,
        UpdateDefaultCoinArgs request)
    {
        var accountLogged = this.GetAccountLogged();
     
        await _appService
            .UpdateDefaultCoinByIdAsync(
            id,
            request,
            accountLogged)
            .ConfigureAwait(false);
    }
}
