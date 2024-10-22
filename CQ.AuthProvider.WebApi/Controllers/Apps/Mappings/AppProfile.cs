using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Apps.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps.Mappings;

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
