using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public interface IBlobService
{
    Task<BlobReadWriteResponse> CreateAsync(
        CreateBlobRequest request,
        AccountLogged accountLogged);

    BlobReadResponse GetByKey(string key);

    Task<string> MoveObjectAsync(
        string key,
        string oldApp,
        string newApp);
}
