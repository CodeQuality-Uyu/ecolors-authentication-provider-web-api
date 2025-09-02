namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CreateAppArgs(
    string Name,
    bool IsDefault,
    Guid CoverId,
    CreateAppCoverBackgroundColorArgs? BackgroundColors,
    Guid? BackgroundCoverId,
    Coin DefaultCoin,
    bool AddToApp);

public sealed record CreateAppCoverBackgroundColorArgs(
    List<string> Colors,
    string Config);

public sealed record UpdateDefaultCoinArgs(Coin DefaultCoin);