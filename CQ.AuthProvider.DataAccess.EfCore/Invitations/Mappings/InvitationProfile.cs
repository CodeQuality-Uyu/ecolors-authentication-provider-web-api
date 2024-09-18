using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations.Mappings;

internal sealed class InvitationProfile
    : Profile
{
    public InvitationProfile()
    {
        #region Create
        CreateMap<Invitation, InvitationEfCore>()
            .ConvertUsing((source, destination, context) => new InvitationEfCore(source));
        #endregion

        #region GetAll
        CreateMap<InvitationEfCore, Invitation>()
        #region Accept
            .ForMember(
            destination => destination.App,
            options => options.MapFrom(
                source => new App()
                {
                    Id = source.AppId,
                    Tenant = new Tenant
                    {
                        Id = source.TenantId
                    }
                }))
            .ForMember(
            destination => destination.Role,
            options => options.MapFrom(
                source => new Role()
                {
                    Id = source.RoleId,
                }))
        #endregion
            ;
        this.CreateOnlyPaginationMap<InvitationEfCore, Invitation>();
        #endregion
    }
}
