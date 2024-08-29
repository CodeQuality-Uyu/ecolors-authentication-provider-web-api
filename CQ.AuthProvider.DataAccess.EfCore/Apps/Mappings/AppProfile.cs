using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps.Mappings;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        CreateMap<AppEfCore, App>();

        CreateMap<AccountApp, App>()
            .ConvertUsing((source, destination, options) => options.Mapper.Map<App>(source.App));
    }
}
