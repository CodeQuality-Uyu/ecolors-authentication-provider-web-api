namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed record BlobReadWriteResponse(
    string Key,
    string ReadUrl,
    string WriteUrl);
