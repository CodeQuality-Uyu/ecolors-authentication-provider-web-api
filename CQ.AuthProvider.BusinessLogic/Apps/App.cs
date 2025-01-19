using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record class App()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;
    
    public bool IsDefault { get; set; }

    public Guid CoverId { get; init; }

    public string? BackgroundCoverColorHex { get; init; }

    public Guid? BackgroundCoverId { get; init; }

    public Tenant Tenant { get; init; } = null!;

    public App(
        string name,
        bool isDefault,
        string? backgroundCoverColorHex,
        Guid? backgroundCoverId,
        Tenant tenant)
        : this()
    {
        Name = Guard.Normalize(name);
        IsDefault = isDefault;
        Tenant = tenant;
        BackgroundCoverColorHex = backgroundCoverColorHex;
        BackgroundCoverId = backgroundCoverId;
    }
}
