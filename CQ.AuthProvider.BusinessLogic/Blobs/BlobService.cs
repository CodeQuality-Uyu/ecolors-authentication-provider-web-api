using Amazon.S3;
using Amazon.S3.Model;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

internal sealed class BlobService(IAmazonS3 _client)
    : IBlobService
{
    public BlobRead GetReadProfilePicture(
        Guid profilePictureId,
        Guid accountId,
        string appName,
        string tenantName)
    {
        var bucketName = tenantName.ToLower().Replace(" ", "-");
        var appFolder = appName.ToLower();

        var key = $"{appFolder}/accounts/{accountId}/${profilePictureId}";

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
        var appFolder = app.Name.ToLower();

        var key = $"{appFolder}/{app.Id}/${elementId}";

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobRead(
            app.Id,
            key,
            readUrl);
    }

    public BlobRead GetReadElementInTenant(
        Tenant tenant,
        Guid elementId)
    {
        var bucketName = tenant.Name.ToLower().Replace(" ", "-");

        var key = $"${elementId}";

        var readUrl = GeneratePresignedUrl(key, bucketName, HttpVerb.GET);

        return new BlobRead(
            elementId,
            key,
            readUrl);
    }
}
