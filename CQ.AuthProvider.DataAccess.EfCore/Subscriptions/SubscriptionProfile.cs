using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Subscriptions;

namespace CQ.AuthProvider.DataAccess.EfCore.Subscriptions;

public sealed class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<SubscriptionEfCore, Subscription>();
    }
}