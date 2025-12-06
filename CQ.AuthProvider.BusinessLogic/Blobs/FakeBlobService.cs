using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Blobs;
public sealed class FakeBlobService
    : IBlobService
{
    public Task<BlobReadWriteResponse> CreateAsync(CreateBlobRequest request, AccountLogged accountLogged)
    {
        return Task.FromResult(
            new BlobReadWriteResponse(
                "fake-key",
                "https://fake-read-url",
                "https://fake-write-url"));
    }

    public BlobReadResponse GetByKey(string key)
    {
        return new BlobReadResponse
        {
            Key = key,
            Url = "https://fake-read-url"
        };
    }

    public Task<string> MoveObjectAsync(
        string key,
        string oldApp,
        string newApp)
    {
        return Task.FromResult("fake-new-key");
    }
}
