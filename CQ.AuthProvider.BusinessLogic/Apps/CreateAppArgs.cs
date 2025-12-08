namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    Logo? Logo,
    Background? Background,
    string? BackgroundCoverKey,
    bool RegisterToIt = false);

public sealed record CreateClientAppArgs(
    string Name,
    Logo? Logo,
    Background? Background);
