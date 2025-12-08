using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record class App()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public bool IsDefault { get; set; }

    public Logo Logo { get; init; } = null!;

    public Background? Background { get; init; }

    public Tenant Tenant { get; init; } = null!;

    public App? FatherApp { get; init; } = null!;

    public App(
        string name,
        bool isDefault,
        Logo logo,
        Background? background,
        Tenant tenant,
        App? fatherApp)
        : this()
    {
        Name = Guard.Normalize(name);
        IsDefault = isDefault;
        Tenant = tenant;
        Logo = logo;
        Background = background;
        FatherApp = fatherApp;
    }
}


public sealed record Logo
{
    public string ColorKey { get; set; } = null!;

    public string LightKey { get; set; } = null!;
    
    public string DarkKey { get; set; } = null!;
}

public sealed record Background
{
    public IList<string> Colors { get; set; } = [];
    
    public string? Config { get; set; }

    public string? BackgroundKey { get; set; }
}
