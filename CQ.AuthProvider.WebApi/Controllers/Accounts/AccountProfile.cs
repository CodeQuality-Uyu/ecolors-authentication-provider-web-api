using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Sessions;
using CQ.AuthProvider.WebApi.Controllers.Tenants;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        this.CreatePaginationMap<Account, AccountBasicInfoResponse>();

        #region Create
        CreateMap<CreateAccountResult, SessionCreatedResponse>()
            .ConvertUsing((source,destination,options)=>new SessionCreatedResponse(
                source.Id,
                source.ProfilePictureId,
                source.Email,
                source.FirstName,
                source.LastName,
                source.FullName,
                source.Token,
                source.Roles,
                source.Permissions,
                options.Mapper.Map<TenantOfAccountBasicInfoResponse>(source.Tenant)));
        #endregion

        #region Create credentials for
        CreateMap<CreateAccountResult, CreateCredentialsForResponse>();
        #endregion
    }
}
