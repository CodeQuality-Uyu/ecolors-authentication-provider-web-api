using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core.Domain
{
    [BsonIgnoreExtraElements]
    public class ResetPasswordRequest
    {
        [BsonId]
        public string Id { get; set; }

        public MiniUser User { get; set; }

        public string Code { get; set; }

        public ResetPasswordRequest()
        {
            Id = Guid.NewGuid().ToString().Replace("-","");
        }
    }

    [BsonIgnoreExtraElements]
    public class MiniUser
    {
        [BsonId]
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
