using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Mappings;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        this.CreatePaginationMap<Account, AccountBasicInfoResponse>();
    }
}
