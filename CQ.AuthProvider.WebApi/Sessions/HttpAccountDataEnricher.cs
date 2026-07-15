using System.Text.Json;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Sessions;
using Flurl;
using Flurl.Http;

namespace CQ.AuthProvider.WebApi.Sessions;

internal sealed class HttpAccountDataEnricher(
    ILogger<HttpAccountDataEnricher> _logger)
    : IAccountDataEnricher
{
    private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(2);

    public async Task<object?> GetAsync(App app, string token)
    {
        var source = app.AccountDataSource;
        if (source is null)
        {
            return null;
        }

        try
        {
            var json = await source.Host
                .AppendPathSegment(source.Endpoint)
                .WithOAuthBearerToken(token)
                .WithTimeout(_timeout)
                .GetStringAsync()
                .ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            // Kept opaque: deserialized as a JsonElement and re-serialized as-is
            // in the login response, so the auth provider never models the shape.
            return JsonSerializer.Deserialize<JsonElement>(json);
        }
        catch (FlurlHttpException ex)
        {
            _logger.LogWarning(
                "Account data enrichment for app {AppId} failed with status {StatusCode}",
                app.Id,
                ex.StatusCode);

            return null;
        }
        catch (Exception ex)
        {
            // Enrichment must never break login: degrade to no extra data.
            _logger.LogWarning(
                ex,
                "Account data enrichment for app {AppId} failed",
                app.Id);

            return null;
        }
    }
}
