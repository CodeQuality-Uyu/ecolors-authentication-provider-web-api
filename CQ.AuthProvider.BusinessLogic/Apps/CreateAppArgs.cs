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

public sealed record CreateAppCoverBackgroundColorArgs
{
    public List<string> Colors { get; init; } = [];

    public string Config { get; init; } = null!;
}