namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

public sealed record BlobCreatedResponse(
    Guid Id,
    string ReadUrl,
    string WriteUrl);
