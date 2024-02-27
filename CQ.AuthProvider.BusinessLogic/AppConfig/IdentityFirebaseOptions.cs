using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed class IdentityFirebaseOptions : IdentityOptions
    {
        public string ProjectName { get; set; } = string.Empty;

        public string ProjectId { get; set; } = string.Empty;

        public string PrivateKeyId { get; set; } = string.Empty;

        public string PrivateKey { get; set; } = string.Empty;

        public string ClientEmail { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string AuthUri { get; set; } = string.Empty;

        public string TokenUri { get; set; } = string.Empty;

        public string AuthProvider { get; set; } = string.Empty;

        public string ClientCert { get; set; } = string.Empty;

        public string UniverseDomain { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public string ApiUrl { get; set; } = string.Empty;

        public string RefererApiUrl { get; set; } = string.Empty;
    }
}
