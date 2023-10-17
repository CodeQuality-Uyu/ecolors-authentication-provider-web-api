using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal interface ISettingsService
    {
        string? GetValue(EnvironmentVariable variable);
    }
}
