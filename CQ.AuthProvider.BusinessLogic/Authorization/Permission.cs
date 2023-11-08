using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed class Permission
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Key { get; init; }

        public bool IsPublic { get; init; }

        public Permission()
        {
            Id = Db.NewId();
        }

        public Permission(string name, string description, string key, bool isPublic)
        {
            Id = Db.NewId();
            Name = name;
            Description = description;
            Key = key;
            IsPublic = isPublic;
        }
    }
}
