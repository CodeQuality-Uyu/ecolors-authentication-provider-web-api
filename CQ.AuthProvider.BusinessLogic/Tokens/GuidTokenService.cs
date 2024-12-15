using CQ.AuthProvider.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Tokens;

public sealed class GuidTokenService
    : ITokenService
{
    public string AuthorizationTypeHandled => "Bearer";

    public Task<string> CreateAsync(object item)
    {
        return Task.FromResult(Db.NewId());
    }

    public Task<bool> IsValidAsync(string value)
    {
        var isGuid = Db.IsIdValid(value);

        return Task.FromResult(isGuid);
    }
}
