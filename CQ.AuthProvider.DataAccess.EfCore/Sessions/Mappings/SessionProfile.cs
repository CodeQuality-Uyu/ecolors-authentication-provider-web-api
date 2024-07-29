using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

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
                Account = options.Mapper.Map<Account>(source.Account)
            });
    }
}
