namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed record CreateBlobRequest(
    Guid? AppId,
    string? Key,
    string ContentType);
