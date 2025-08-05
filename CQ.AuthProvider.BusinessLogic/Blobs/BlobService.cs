using Amazon.S3;
using Amazon.S3.Model;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public sealed class BlobService(IAmazonS3 _client)
    : IBlobService
{
    public BlobRead GetReadProfilePicture(
        Guid profilePictureId,
        Guid accountId,
        string appName,
        string tenantName)
    {
        var bucketName = tenantName.ToLower().Replace(" ", "-");
        var appFolder = appName.ToLower().Replace(" ", "-");

        var key = $"{appFolder}/accounts/{accountId}/{profilePictureId}";

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobRead(
            profilePictureId,
            key,
            readUrl);
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

    public BlobRead GetReadElementInApp(
        App app,
        Guid elementId)
    {
        var bucketName = app.Tenant.Name.ToLower().Replace(" ", "-");

        var key = $"{app.Name.ToLower().Replace(" ", "-")}/{elementId}";

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobRead(
            elementId,
            key,
            readUrl);
    }

    public BlobRead GetReadElementInTenant(
        Tenant tenant,
        Guid elementId)
    {
        var bucketName = tenant.Name.ToLower().Replace(" ", "-");

        var key = $"{elementId}";

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobRead(
            elementId,
            key,
            readUrl);
    }

    public async Task MoveAppElementAsync(
        App oldApp,
        App newApp,
        Guid elementId)
    {
        var bucketName = newApp.Tenant.Name.ToLower().Replace(" ", "-");

        var oldKey = $"{oldApp.Name.ToLower().Replace(" ", "-")}/upload/{elementId}";

        var newKey = $"{newApp.Name.ToLower().Replace(" ", "-")}/{elementId}";

        var copyRequest = new CopyObjectRequest
        {
            SourceBucket = bucketName,
            SourceKey = oldKey,
            DestinationBucket = bucketName,
            DestinationKey = newKey
        };

        await _client
            .CopyObjectAsync(copyRequest)
            .ConfigureAwait(false);

        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = oldKey
        };

        await _client
            .DeleteObjectAsync(deleteRequest)
            .ConfigureAwait(false);
    }
}
