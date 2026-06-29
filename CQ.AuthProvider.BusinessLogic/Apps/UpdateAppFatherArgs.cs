namespace CQ.AuthProvider.BusinessLogic.Apps;

/// <summary>
/// Re-parents an app under another app (or makes it top-level when null).
/// Used for app/permission migration scenarios.
/// </summary>
public sealed record UpdateAppFatherArgs(Guid? FatherAppId);
