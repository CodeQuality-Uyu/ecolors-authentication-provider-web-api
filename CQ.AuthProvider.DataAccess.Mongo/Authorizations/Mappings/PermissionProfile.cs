using AutoMapper;
using CQ.AuthProvider.DataAccess.Mongo.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations.Mappings
{
    public sealed class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionEfCore, Permission>()
                .ForMember(destination => destination.Key,
                options => options.MapFrom(
                    source => new PermissionKey(source.Key)));

            CreateMap<PermissionMongo, Permission>()
                .ForMember(destination => destination.Key,
                options => options.MapFrom(
                    source => new PermissionKey(source.Key)));

            CreateMap<List<PermissionEfCore>, List<Permission>>()
                .ConvertUsing((source, destination, context) =>
                source.Select(p => context.Mapper.Map<Permission>(p)).ToList());

            CreateMap<List<PermissionMongo>, List<Permission>>()
                .ConvertUsing((source, destination, context) =>
                source.Select(p => context.Mapper.Map<Permission>(p)).ToList());
        }
    }
}
