using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class NewPasswordRequest
    {
        public string NewPassword { get; set; }

        public string Code { get;set; }

        public string Email { get; set; }
    }
}
