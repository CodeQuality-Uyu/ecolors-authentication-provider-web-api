using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        #region GetAll
        this.CreatePaginationMap<App, AppBasicInfoResponse>();
        #endregion
    }
}
