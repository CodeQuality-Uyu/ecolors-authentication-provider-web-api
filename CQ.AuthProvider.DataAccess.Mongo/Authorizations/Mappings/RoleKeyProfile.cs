using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations.Mappings
{
    internal sealed class RoleKeyProfile : Profile
    {
        public RoleKeyProfile()
        {
            CreateMap<List<RoleKey>, List<string>>()
                .ConvertUsing(source => source.Select(r => r.ToString()).ToList());
        }
    }
}
