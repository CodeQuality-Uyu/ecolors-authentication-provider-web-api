using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>().MaxDepth(2);
        this.CreateOnlyPaginationMap<AccountEfCore, Account>();
    }
}
