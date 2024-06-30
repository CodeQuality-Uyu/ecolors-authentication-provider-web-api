using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts.Mappings;

public sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountMongo, Account>()
            .ForMember(
            destination => destination.Roles,
            options => options.MapFrom(
                source => source.Roles.Select(r => r.Key)));
    }
}
