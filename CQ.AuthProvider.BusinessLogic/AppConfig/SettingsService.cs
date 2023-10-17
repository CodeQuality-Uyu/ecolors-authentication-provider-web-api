using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal class SettingsService : ISettingsService
    {
        public string? GetValue(EnvironmentVariable variable)
        {
            var value = Environment.GetEnvironmentVariable(variable.Value);

            return value;
        }
    }
}
