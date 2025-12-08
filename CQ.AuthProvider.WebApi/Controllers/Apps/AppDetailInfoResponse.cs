using CQ.AuthProvider.BusinessLogic.Blobs;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public readonly struct AppDetailInfoResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public LogoResponse Logo { get; init; }

    public BackgroundResponse? Background { get; init; }
}

public readonly struct LogoResponse
{
    public BlobReadResponse Color { get; init; }

    public BlobReadResponse Light { get; init; }

    public BlobReadResponse Dark { get; init; }
}

public readonly struct BackgroundResponse()
{
    public BlobReadResponse? BackgroundKey { get; init; }

    public IList<string> Colors { get; init; } = [];

    public string? Config { get; init; }
}
