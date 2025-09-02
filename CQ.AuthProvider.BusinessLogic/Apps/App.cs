﻿using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record class App()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public bool IsDefault { get; set; }

    public Guid CoverId { get; init; }

    public CoverBackgroundColor? BackgroundColor { get; init; }

    public Guid? BackgroundCoverId { get; init; }

    public Tenant Tenant { get; init; } = null!;

    public App? FatherApp { get; init; } = null!;

    public Guid DefaultCoingId { get; init; }
    
    public Coin DefaultCoin { get; init; }

    public App(
        string name,
        bool isDefault,
        Guid coverId,
        CoverBackgroundColor? backgroundColor,
        Guid? backgroundCoverId,
        Tenant tenant,
        App? fatherApp,
        Guid defaultCoinId)
        : this()
    {
        Name = Guard.Normalize(name);
        IsDefault = isDefault;
        Tenant = tenant;
        CoverId = coverId;
        BackgroundColor = backgroundColor;
        BackgroundCoverId = backgroundCoverId;
        FatherApp = fatherApp;
        DefaultCoinId = defaultCoinId;
    }
}
