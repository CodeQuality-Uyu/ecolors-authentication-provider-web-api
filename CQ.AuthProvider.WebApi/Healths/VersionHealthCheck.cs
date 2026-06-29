using CQ.AuthProvider.WebApi.AppConfig;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.WebApi.Healths;

internal sealed class VersionHealthCheck(
    IOptions<VersionSection> versionOptions)
    : IHealthCheck
{
    private readonly VersionSection versionSection = versionOptions.Value;

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var data = new Dictionary<string, object?>
        {
            { "version", versionSection.Version },
            { "buildDate", versionSection.BuildDate },
            { "prLink", versionSection.PrLink }
        };

        return Task.FromResult(
            HealthCheckResult.Healthy("The API is running.", data!));
    }
}
