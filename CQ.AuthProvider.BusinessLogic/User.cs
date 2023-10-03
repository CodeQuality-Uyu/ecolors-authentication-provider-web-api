using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core.Domain
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
