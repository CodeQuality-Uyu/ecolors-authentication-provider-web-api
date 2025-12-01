using AutoMapper;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Utils;

public static class ProfileExtensions
{
    public static Profile CreatePaginationMap<TSource, TDestination>(this Profile profile)
    {
        profile
            .CreateMap<TSource, TDestination>();

        profile
            .CreateMap<Pagination<TSource>, Pagination<TDestination>>()
            .ConvertUsing((source, destination, context) => new Pagination<TDestination>(context.Mapper.Map<List<TDestination>>(source.Items), source.TotalCount, source.TotalPages, source.Page, source.PageSize));

        return profile;
    }

    public static Profile CreateOnlyPaginationMap<TSource, TDestination>(this Profile profile)
    {
        profile
            .CreateMap<Pagination<TSource>, Pagination<TDestination>>()
            .ConvertUsing((source, destination, context) => new Pagination<TDestination>(context.Mapper.Map<List<TDestination>>(source.Items), source.TotalCount, source.TotalPages, source.Page, source.PageSize));

        return profile;
    }
}
