using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions.Mappings;

internal sealed class SessionProfile
    : Profile
{
    public SessionProfile()
    {
        CreateMap<SessionEfCore, Session>()
            .ConvertUsing((source, destination, options) => new Session
            {
                Id = source.Id,
                Token = source.Token,
                Account = options.Mapper.Map<Account>(source.Account),
                App = new App
                {
                    Id = source.AppId,
                    Tenant = new Tenant
                    {
                        Id = source.Account.TenantId
                    }
                },
            });
    }
}
