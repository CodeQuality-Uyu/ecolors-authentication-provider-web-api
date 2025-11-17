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
internal sealed class BlobController(
    IAmazonS3 client)
    : ControllerBase
{
    [HttpPost]
    public async Task<BlobReadWriteResponse> CreateAsync(CreateBlobRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        var bucketName = accountLogged.Tenant.Name.ToLower().Replace(" ", "-");

        var key = request.Key;
        var id = Guid.NewGuid();
        if (Guard.IsNullOrEmpty(request.Key))
        {
            var appFolder = (Guard.IsNotNull(request.AppId)
                ? accountLogged.Apps.FirstOrDefault(a => a.Id == request.AppId)?.Name ?? accountLogged.AppLogged.Name
                : accountLogged.AppLogged.Name)
                .ToLower()
                .Replace(" ", "-");

            key = $"{appFolder}/upload/{id}";

            await EnsureBucketExistsAsync(bucketName, appFolder).ConfigureAwait(false);
        }
        else
        {
            var keySplitted = key.Split("/");
            var keyId = keySplitted[keySplitted.Length - 1];
            id = Guid.Parse(keyId);
        }

        var readUrl = GeneratePresignedUrl(
            key,
            bucketName,
            HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl(
            key,
            bucketName,
            HttpVerb.PUT,
            request.ContentType);

        return new BlobReadWriteResponse(
            id,
            key,
            readUrl,
            writeUrl);
    }

    private async Task EnsureBucketExistsAsync(string bucketName, string uploadFolder)
    {
        var existBucket = await AmazonS3Util
            .DoesS3BucketExistV2Async(client, bucketName)
            .ConfigureAwait(false);

        if (existBucket)
        {
            return;
        }

        var createBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
        };
        await client
            .PutBucketAsync(createBucketRequest)
            .ConfigureAwait(false);

        var publicAccessBlockRequest = new PutPublicAccessBlockRequest
        {
            BucketName = bucketName,
            PublicAccessBlockConfiguration = new PublicAccessBlockConfiguration
            {
                BlockPublicAcls = false,
                IgnorePublicAcls = false,
                BlockPublicPolicy = false,
                RestrictPublicBuckets = false
            }
        };
        await client
            .PutPublicAccessBlockAsync(publicAccessBlockRequest)
            .ConfigureAwait(false);

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
            Policy = policy,
        };
        await client
            .PutBucketPolicyAsync(createBucketPolicyRequest)
            .ConfigureAwait(false);
    }

    private string GeneratePresignedUrl(
        string key,
        string bucketName,
        HttpVerb verb,
        string? contentType = null)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = key,
            Verb = verb,
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        return client.GetPreSignedURL(request);
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
