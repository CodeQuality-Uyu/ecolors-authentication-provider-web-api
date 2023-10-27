using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed class SettingsService : ISettingsService
    {
        public string GetValue(EnvironmentVariable variable)
        {
            var value = Environment.GetEnvironmentVariable(variable.Value);

            Guard.ThrowIsNullOrEmpty(value, variable.Value);

            return value;
        }

        public string? GetValueOrDefault(EnvironmentVariable variable)
        {
            var value = Environment.GetEnvironmentVariable(variable.Value);

            return value;
        }
    }
}
