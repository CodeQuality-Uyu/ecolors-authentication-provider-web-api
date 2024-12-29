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

        #region GetById
        CreateMap<App, AppDetailInfoResponse>();
            //.ForMember(destination => destination.CoverMultimedia,
            //options => options.MapFrom);

        #endregion
    }
}
