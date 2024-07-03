using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Mappings;

internal sealed class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        #region Get all
        CreateMap<Permission, PermissionBasicInfoResponse>();
        CreateMap<PermissionKey, string>()
            .ConvertUsing((source, destination, context) => source.ToString());
        #endregion
    }
}
