using CQ.AuthProvider.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Tokens;

public sealed class GuidTokenService
    : ITokenService
{
    public Task<string> CreateAsync(object item)
    {
        return Task.FromResult(Db.NewId());
    }

    public Task<bool> IsValidAsync(string header, string value)
    {
        var isGuid = Db.IsIdValid(value);

        return Task.FromResult(isGuid);
    }
}
