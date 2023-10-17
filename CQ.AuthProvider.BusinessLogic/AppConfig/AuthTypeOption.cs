using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal static class AuthTypeOption
    {
        public static readonly string Firebase = "firebase";
        public static readonly string OAuth2 = "oauth2";
        public static readonly string Custom = "custom";
    }
}
