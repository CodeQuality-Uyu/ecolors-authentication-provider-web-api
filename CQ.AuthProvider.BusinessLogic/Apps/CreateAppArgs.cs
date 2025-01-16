namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    Guid CoverId,
    string? BackgroundCoverColorHex);
