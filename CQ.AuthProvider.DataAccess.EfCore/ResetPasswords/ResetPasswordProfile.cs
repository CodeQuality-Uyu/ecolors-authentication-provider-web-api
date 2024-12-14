using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
internal sealed class ResetPasswordProfile
    : Profile
{
    public ResetPasswordProfile()
    {
        CreateMap<ResetPasswordEfCore, ResetPassword>()
            .ForMember(
            destination => destination.Account,
            options => options.MapFrom(
                source => new Account
                {
                    Id = source.AccountId
                }));
    }
}
