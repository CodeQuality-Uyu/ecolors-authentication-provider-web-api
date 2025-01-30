using Amazon.S3;
using Amazon.S3.Model;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.AppConfig;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.DataAccess.EfCore.Blobs;

[ApiController]
[Route("blobs")]
[BearerAuthentication]
public sealed class BlobController(
    IAmazonS3 _client,
    IOptions<BlobConfiguration> blobConfiguration)
    : ControllerBase
{
    [HttpPost]
    public Task<BlobCreatedResponse> CreateAsync()
    {
        var id = Guid.NewGuid();
        var readUrl = GeneratePresignedUrl($"upload/{id}", HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl($"upload/{id}", HttpVerb.PUT);

        return Task.FromResult(new BlobCreatedResponse(id, readUrl, writeUrl));
    }

    private string GeneratePresignedUrl(string objectKey, HttpVerb verb)
    {
        var accountLogged = this.GetAccountLogged();

        var request = new GetPreSignedUrlRequest
        {
            BucketName = accountLogged.Tenant.Name.ToLower().Replace(" ", "-"),
            Key = objectKey,
            Verb = verb,
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        return _client.GetPreSignedURL(request);
    }
}
