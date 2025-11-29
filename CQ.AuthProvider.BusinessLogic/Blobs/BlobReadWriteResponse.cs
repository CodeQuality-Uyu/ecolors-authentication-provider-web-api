namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed record BlobReadWriteResponse(
    Guid Id,
    string Key,
    string ReadUrl,
    string WriteUrl);
