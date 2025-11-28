using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record class App()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public bool IsDefault { get; set; }

    public string CoverKey { get; init; } = null!;

    public CoverBackgroundColor? BackgroundColor { get; init; }

    public string? BackgroundCoverKey { get; init; }

    public Tenant Tenant { get; init; } = null!;

    public App? FatherApp { get; init; } = null!;

    public App(
        string name,
        bool isDefault,
        string coverKey,
        CoverBackgroundColor? backgroundColor,
        string? backgroundCoverKey,
        Tenant tenant,
        App? fatherApp)
        : this()
    {
        Name = Guard.Normalize(name);
        IsDefault = isDefault;
        Tenant = tenant;
        CoverKey = coverKey;
        BackgroundColor = backgroundColor;
        BackgroundCoverKey = backgroundCoverKey;
        FatherApp = fatherApp;
    }
}
