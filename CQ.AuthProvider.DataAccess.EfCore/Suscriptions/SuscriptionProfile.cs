using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Suscriptions;

namespace CQ.AuthProvider.DataAccess.EfCore.Suscriptions;

public sealed class SuscriptionProfile : Profile
{
    public SuscriptionProfile()
    {
        CreateMap<SuscriptionEfCore, Suscription>();
    }
}