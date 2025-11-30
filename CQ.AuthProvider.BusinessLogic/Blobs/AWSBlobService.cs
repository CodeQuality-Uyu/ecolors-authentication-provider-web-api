using Amazon.S3;
using Amazon.S3.Model;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.Utility;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed class AWSBlobService(
    IAmazonS3 client,
    IOptions<BlobSection> options)
    : IBlobService
{
    private readonly BlobSection blobOptions = options.Value;
    
    public async Task<BlobReadWriteResponse> CreateAsync(
        CreateBlobRequest request,
        AccountLogged accountLogged)
    {
        var tenantName = accountLogged
        .Tenant
        .Name
        .ToLower()
        .Trim()
        .Replace(" ", "-");

        var key = request.Key;
        var id = Guid.NewGuid();
        if (Guard.IsNullOrEmpty(request.Key))
        {
            var appName = accountLogged.AppLogged.Name;
            if(Guard.IsNotNull(request.AppId))
            {
                var appExists = accountLogged
                .Apps
                .FirstOrDefault(a => a.Id == request.AppId)
                ?? throw new InvalidOperationException("The app does not belong to the logged account.");

                appName = appExists.Name;
            }
            appName = appName.ToLower().Trim().Replace(" ", "-");

            key = $"{blobOptions.TemporaryObject}/{tenantName}/{appName}/{id}.{request.ContentType}";
        }
        else
        {
            key = $"{key}/{Guid.NewGuid()}.{request.ContentType}";
        }

        var readUrl = GeneratePresignedUrl(
            key,
            HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl(
            key,
            HttpVerb.PUT,
            request.ContentType);

        return new BlobReadWriteResponse(
            id,
            key,
            readUrl,
            writeUrl);
    }


    private string GeneratePresignedUrl(
        string key,
        HttpVerb verb,
        string? contentType = null)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = blobOptions.BucketName,
            Key = key,
            Verb = verb,
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        return client.GetPreSignedURL(request);
    }

    public BlobReadResponse GetByKey(
        string key)
    {
        var readUrl = GeneratePresignedUrl(key, HttpVerb.GET);

        return new BlobReadResponse
        {
            Key = key,
            Url = readUrl
        };
    }

    public async Task MoveObjectAsync(
        string oldKey,
        string newKey)
    {
        newKey = oldKey.Replace($"{blobOptions.TemporaryObject}/", string.Empty);

        var copyRequest = new CopyObjectRequest
        {
            SourceBucket = blobOptions.BucketName,
            SourceKey = oldKey,
            DestinationBucket = blobOptions.BucketName,
            DestinationKey = newKey
        };

        await client
            .CopyObjectAsync(copyRequest)
            .ConfigureAwait(false);

        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = blobOptions.BucketName,
            Key = oldKey
        };

        await client
            .DeleteObjectAsync(deleteRequest)
            .ConfigureAwait(false);
    }
}
