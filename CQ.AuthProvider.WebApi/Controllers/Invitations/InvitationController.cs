using AutoMapper;
using CQ.ApiElements;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Invitations.Models;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations;

[ApiController]
[Route("invitations")]
public sealed class InvitationController(
    IMapper _mapper,
    IInvitationService _invitationService)
    : ControllerBase
{
    [HttpPost]
    [BearerAuthentication]
    [SecureAuthorization(ContextItem.AccountLogged)]
    public async Task CreateAsync(CreateInvitationArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _invitationService
            .CreateAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    [BearerAuthentication]
    [SecureAuthorization(ContextItem.AccountLogged)]
    public async Task<Pagination<InvitationBasicInfoResponse>> GetAllAsync(
        [FromQuery] Guid? creatorId,
        [FromQuery] Guid? appId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var invitations = await _invitationService
            .GetAllAsync(
            creatorId,
            appId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<InvitationBasicInfoResponse>>(invitations);
    }

    [HttpPut("{id}/accept")]
    public async Task<AccountCreatedResponse> AcceptAsync(
        Guid id,
        AcceptInvitationRequest request)
    {
        var args = request.Map();

        var account = await _invitationService
        .AcceptByIdAsync(
        id,
            args)
            .ConfigureAwait(false);


        return _mapper.Map<AccountCreatedResponse>(account);
    }

    [HttpPut("{id}/declain")]
    public async Task DeclainAsync(
        Guid id,
        DeclainInvitationRequest request)
    {
        var args = request.Map();

        await _invitationService
            .DeclainByIdAsync(
            id,
            args)
            .ConfigureAwait(false);
    }
}
