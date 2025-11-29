using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.DataAccess.EfCore.Blobs;

[ApiController]
[Route("blobs")]
[BearerAuthentication]
internal sealed class BlobController(
    IBlobService blobService)
    : ControllerBase
{
    [HttpPost]
    public async Task<BlobReadWriteResponse> CreateAsync(CreateBlobRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        var result = await blobService
        .CreateAsync(request, accountLogged)
        .ConfigureAwait(false);

        return result;
    }

    [HttpGet("{key}")]
    public BlobReadResponse GetByKey(string key)
    {
        var result = blobService.GetByKey(key);

        return result;
    }
}
