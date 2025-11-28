using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public interface IBlobService
{
    Task<BlobReadWriteResponse> CreateAsync(
        CreateBlobRequest request,
        AccountLogged accountLogged);

    BlobReadResponse GetByKey(
        string key,
        string bucketName = "blobs");

    Task MoveObjectAsync(
        string oldKey,
        string newKey);
}
