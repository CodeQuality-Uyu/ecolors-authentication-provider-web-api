using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Blobs;
public sealed class FakeBlobService
    : IBlobService
{
    public BlobRead GetReadElementInApp(App app, Guid elementId)
    {
        return new BlobRead(elementId, "fake key", "fake url");
    }

    public BlobRead GetReadElementInTenant(Tenant tenant, Guid elementId)
    {
        return new BlobRead(elementId, "fake key", "fake url");
    }

    public BlobRead GetReadProfilePicture(Guid profilePictureId, Guid accountId, string appName, string tenantName)
    {
        return new BlobRead(profilePictureId, "fake key", "fake url");
    }

    public Task MoveAppElementAsync(App oldApp, App newApp, Guid elementId)
    {
        return Task.CompletedTask;
    }
}
