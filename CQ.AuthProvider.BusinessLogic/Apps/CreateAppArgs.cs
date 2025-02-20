namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    Guid CoverId,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    Guid? BackgroundCoverId);

public sealed record CreateAppCoverBackgroundColorArgs(
    List<string> Colors,
    string Config);