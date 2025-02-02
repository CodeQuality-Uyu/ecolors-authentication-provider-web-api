namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

public sealed record BlobReadWriteResponse(
    Guid Id,
    string Key,
    string ReadUrl,
    string WriteUrl);
