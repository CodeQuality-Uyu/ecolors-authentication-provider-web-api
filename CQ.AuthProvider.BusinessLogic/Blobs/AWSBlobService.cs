using Amazon.S3;
using Amazon.S3.Model;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed class AWSBlobService(IAmazonS3 client)
    : IBlobService
{
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

            key = $"{tenantName}/{appName}/temporary/{id}.{request.ContentType}";
        }
        else
        {
            key = $"{key}/{Guid.NewGuid()}.{request.ContentType}";
        }

        var readUrl = GeneratePresignedUrl(
            key,
            "blobs",
            HttpVerb.GET);
        var writeUrl = GeneratePresignedUrl(
            key,
            "blobs",
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

    public BlobReadResponse GetByKey(
        string key,
        string bucketName = "blobs")
    {
        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

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
        newKey = oldKey.Replace("temporary", newKey.ToLower().Trim().Replace(" ", "-"));

        var copyRequest = new CopyObjectRequest
        {
            SourceBucket = "blobs",
            SourceKey = oldKey,
            DestinationBucket = "blobs",
            DestinationKey = newKey
        };

        await client
            .CopyObjectAsync(copyRequest)
            .ConfigureAwait(false);

        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = "blobs",
            Key = oldKey
        };

        await client
            .DeleteObjectAsync(deleteRequest)
            .ConfigureAwait(false);
    }
}
