using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.Utility;
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
    public async Task<BlobReadWriteResponse> CreateAsync(CreateBlobRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        var bucketName = accountLogged.Tenant.Name.ToLower().Replace(" ", "-");
        var appFolder = (Guard.IsNotNull(request.AppId)
            ? accountLogged.Apps.First(a => a.Id == request.AppId).Name
            : accountLogged.AppLogged.Name)
            .ToLower();

        await EnsureBucketExistsAsync(bucketName, appFolder).ConfigureAwait(false);

        var id = Guid.NewGuid();
        var key = $"{appFolder}/upload/{id}";


        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.PUT);

        return new BlobReadWriteResponse(
            id,
            key,
            readUrl,
            writeUrl);
    }

    private async Task EnsureBucketExistsAsync(string bucketName, string uploadFolder)
    {
        var existBucket = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName).ConfigureAwait(false);

        if (existBucket)
        {
            return;
        }

        var createBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
        };
        await _client.PutBucketAsync(createBucketRequest);

        var policy = $@"{{
            ""Version"": ""2012-10-17"",
            ""Statement"": [
                {{
                    ""Effect"": ""Allow"",
                    ""Principal"": ""*"",
                    ""Action"": [""s3:GetObject"", ""s3:PutObject""],
                    ""Resource"": ""arn:aws:s3:::{bucketName}/{uploadFolder}/*""
                }}
            ]
        }}";

        var createBucketPolicyRequest = new PutBucketPolicyRequest
        {
            BucketName = bucketName,
            Policy = policy
        };
        await _client
            .PutBucketPolicyAsync(createBucketPolicyRequest)
            .ConfigureAwait(false);
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

        return new BlobReadResponse
        {
            Id = Guid.NewGuid(),
            Key = key,
            Url = readUrl
        };
    }
}
