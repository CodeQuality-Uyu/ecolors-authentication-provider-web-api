using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles.Mappings;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        CreateMap<RoleEfCore, Role>()
            .ForMember(
            destination => destination.App,
            options => options.MapFrom(
                source => new App()
                {
                    Id = source.AppId
                }));

        this.CreateOnlyPaginationMap<RoleEfCore, Role>();
        #endregion
    }
}