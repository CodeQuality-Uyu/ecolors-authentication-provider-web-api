using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>();
    }
}
