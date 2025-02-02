namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

public sealed record BlobReadResponse(
    string Key,
    string ReadUrl);
