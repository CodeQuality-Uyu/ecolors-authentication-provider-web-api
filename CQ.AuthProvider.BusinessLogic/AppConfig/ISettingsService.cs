using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public interface ISettingsService
    {
        string GetValue(EnvironmentVariable variable);

        string? GetValueOrDefault(EnvironmentVariable variable);
    }
}
