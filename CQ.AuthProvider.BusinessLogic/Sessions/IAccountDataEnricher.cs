using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

public interface IAccountDataEnricher
{
    /// <summary>
    /// Calls the app's registered data source (if any) to fetch app-specific
    /// data for the logged account, returning it as an opaque blob to be
    /// forwarded in the login response. Never throws: on missing source, a
    /// failing/unavailable app API, or a timeout it returns null so login is
    /// never blocked by the app's availability.
    /// </summary>
    Task<object?> GetAsync(App app, string token);
}
