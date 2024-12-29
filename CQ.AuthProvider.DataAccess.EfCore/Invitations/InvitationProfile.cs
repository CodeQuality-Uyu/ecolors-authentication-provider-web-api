using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations;

internal sealed class InvitationProfile
    : Profile
{
    public InvitationProfile()
    {
        #region Create
        CreateMap<Invitation, InvitationEfCore>()
            .ForMember(destination => destination.AppId,
            options => options.MapFrom(
                source => source.App.Id))
            .ForMember(destination => destination.App,
            options => options.Ignore())

            .ForMember(destination => destination.RoleId,
            options => options.MapFrom(
                source => source.Role.Id))
            .ForMember(destination => destination.Role,
            options => options.Ignore())

            .ForMember(destination => destination.TenantId,
            options => options.MapFrom(
                source => source.Tenant.Id))
            .ForMember(destination => destination.Tenant,
            options => options.Ignore())

            .ForMember(destination => destination.CreatorId,
            options => options.MapFrom(
                source => source.Creator.Id))
            .ForMember(destination => destination.Creator,
            options => options.Ignore())
            ;
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
