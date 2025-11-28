namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    string CoverKey,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    string? BackgroundCoverKey,
    bool RegisterToIt = false);

public sealed record CreateClientAppArgs(
    string Name,
    string? CoverKey,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    string? BackgroundCoverKey);

public sealed record CreateAppCoverBackgroundColorArgs(
    List<string> Colors,
    string Config);