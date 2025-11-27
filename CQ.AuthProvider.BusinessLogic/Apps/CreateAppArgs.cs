namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    Guid CoverId,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    Guid? BackgroundCoverId,
    bool RegisterToIt = false);

public sealed record CreateClientAppArgs(
    string Name,
    Guid? CoverId,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    Guid? BackgroundCoverId);

public sealed record CreateAppCoverBackgroundColorArgs
{
    public List<string> Colors { get; init; } = [];

    public string Config { get; init; } = null!;
}