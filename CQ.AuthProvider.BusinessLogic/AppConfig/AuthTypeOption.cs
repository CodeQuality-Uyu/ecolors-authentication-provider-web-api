using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal sealed record class AuthTypeOption
    {
        public static AuthTypeOption Firebase = new("firebase");
        public static AuthTypeOption OAuth2 = new("oauth2");
        public static AuthTypeOption Custom = new("custom");

        public string Value { get; }

        public AuthTypeOption(string value)
        {
            this.Value = value;
        }
    }
}
