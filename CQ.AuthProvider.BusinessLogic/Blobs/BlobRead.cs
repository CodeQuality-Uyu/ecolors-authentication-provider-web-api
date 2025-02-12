
namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed record BlobRead(
    Guid Id,
    string Key,
    string ReadUrl);
