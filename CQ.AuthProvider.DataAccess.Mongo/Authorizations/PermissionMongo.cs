using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations
{
    public sealed class PermissionMongo
    {
        public string Id { get; init; } = null!;

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public string Key { get; init; } = null!;

        public bool IsPublic { get; init; }

        public PermissionMongo()
        {
            Id = Db.NewId();
        }

        public PermissionMongo(
            string name,
            string description,
            PermissionKey key,
            bool isPublic = false)
            : this()
        {
            Name = name;
            Description = description;
            Key = key.ToString();
            IsPublic = isPublic;
        }
    }
}
