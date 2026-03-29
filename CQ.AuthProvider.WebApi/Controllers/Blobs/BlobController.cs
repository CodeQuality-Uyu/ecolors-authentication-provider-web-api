using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

[ApiController]
[Route("blobs")]
[BearerAuthentication]
public sealed class BlobController(
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
    public async Task<BlobReadResponse> GetByKey(string key)
    {
        var result = await blobService.GetByKeyAsync(key).ConfigureAwait(false);

        return result;
    }
}
