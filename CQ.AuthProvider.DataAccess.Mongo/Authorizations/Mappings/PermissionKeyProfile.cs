using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations.Mappings
{
    internal sealed class PermissionKeyProfile : Profile
    {
        public PermissionKeyProfile()
        {
            CreateMap<List<PermissionKey>, List<string>>()
                .ConvertUsing(source => source.Select(p => p.ToString()).ToList());
        }
    }
}
