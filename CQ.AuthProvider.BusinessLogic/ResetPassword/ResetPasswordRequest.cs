
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class ResetPasswordRequest
    {
        public string Id { get; set; }

        public MiniUser User { get; set; }

        public string Code { get; set; }

        public ResetPasswordRequest()
        {
            Id = Guid.NewGuid().ToString().Replace("-","");
        }
    }

    public class MiniUser
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
