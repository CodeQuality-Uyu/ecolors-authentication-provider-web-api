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
    
    private const int PresignedUrlExpirationMinutes = 15;
    
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
        if (Guard.IsNullOrEmpty(request.Key))
        {
            var appName = accountLogged.AppLogged.Name;
            if (Guard.IsNotNull(request.AppId))
            {
                var appExists = accountLogged
                .Apps
                .FirstOrDefault(a => a.Id == request.AppId)
                ?? throw new InvalidOperationException("The app does not belong to the logged account.");

                appName = appExists.Name;
            }
            appName = appName.ToLower().Trim().Replace(" ", "-");

            key = $"{blobOptions.TemporaryObject}/{tenantName}/{appName}";
        }
        
        var contentType = request.ContentType.Split("/")[1];
        key = $"{key}/{Guid.NewGuid()}.{contentType}";

        var readUrl = GeneratePresignedUrl(
            key,
            HttpVerb.GET);
        var writeUrl = GenerateUploadPresignedUrl(
            key,
            request.ContentType);

        return new BlobReadWriteResponse(
            key,
            readUrl,
            writeUrl);
    }

private string GeneratePresignedUrl(
    string key,
    HttpVerb verb,
    string? contentType = null,
    bool useServerSideEncryption = false)
{
    var request = new GetPreSignedUrlRequest
    {
        BucketName  = blobOptions.BucketName,
        Key         = key,
        Verb        = verb,
        ContentType = contentType,
        Expires     = DateTime.UtcNow.AddMinutes(PresignedUrlExpirationMinutes),
    };

    if (useServerSideEncryption)
    {
        request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;
    }

    return client.GetPreSignedURL(request);
}

private string GenerateUploadPresignedUrl(string key, string contentType)
    => GeneratePresignedUrl(
        key: key,
        verb: HttpVerb.PUT,
        contentType: contentType,
        useServerSideEncryption: true);

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

    public async Task<string> MoveObjectAsync(
        string key,
        string oldApp,
        string newApp)
    {
        var newKey = key.Replace($"{blobOptions.TemporaryObject}/", string.Empty);
        newKey = newKey.Replace(oldApp.ToLower().Trim().Replace(" ", "-"), newApp.ToLower().Trim().Replace(" ", "-"));

        var copyRequest = new CopyObjectRequest
        {
            SourceBucket = blobOptions.BucketName,
            SourceKey = key,
            DestinationBucket = blobOptions.BucketName,
            DestinationKey = newKey,
            ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256,
        };

        await client
            .CopyObjectAsync(copyRequest)
            .ConfigureAwait(false);

        return newKey;
    }
}
