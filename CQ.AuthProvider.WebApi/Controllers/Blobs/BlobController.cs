using Amazon.S3;
using Amazon.S3.Model;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.DataAccess.EfCore.Blobs;

[ApiController]
[Route("blobs")]
[BearerAuthentication]
public sealed class BlobController(
    IAmazonS3 _client)
    : ControllerBase
{
    [HttpPost]
    public async Task<BlobReadWriteResponse> CreateAsync()
    {
        var accountLogged = this.GetAccountLogged();

        var bucketName = accountLogged.Tenant.Name.ToLower().Replace(" ", "-");

        await _client.EnsureBucketExistsAsync(bucketName);

        var id = Guid.NewGuid();
        var key = $"${accountLogged.AppLogged.Name}/upload/{id}";


        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.PUT);

        return new BlobReadWriteResponse(
            id,
            key,
            readUrl,
            writeUrl);
    }

    private string GeneratePresignedUrl(
        string key,
        string bucketName,
        HttpVerb verb)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = key,
            Verb = verb,
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        return _client.GetPreSignedURL(request);
    }

    [HttpGet("{key}")]
    public BlobReadResponse GetByKey(string key)
    {
        var accountLogged = this.GetAccountLogged();
        
        var bucketName = accountLogged.Tenant.Name.ToLower().Replace(" ", "-");

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobReadResponse(key, readUrl);
    }
}
