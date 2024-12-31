namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public sealed record AppBasicInfoResponse(
    Guid Id,
    string Name,
    bool IsDefault);
