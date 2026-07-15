namespace CQ.AuthProvider.BusinessLogic.Apps;

/// <summary>
/// Updates the editable fields of an app: its name and its optional
/// <see cref="AccountDataSource"/> (null clears the source, disabling login enrichment).
/// </summary>
public sealed record UpdateAppArgs(
    string Name,
    AccountDataSource? AccountDataSource);
