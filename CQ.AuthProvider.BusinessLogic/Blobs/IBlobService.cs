using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Blobs;

public interface IBlobService
{
    BlobRead GetReadProfilePicture(
        Guid profilePictureId,
        Guid accountId,
        string appName,
        string tenantName);

    BlobRead GetReadElementInApp(
        App app,
        Guid elementId);

    BlobRead GetReadElementInTenant(
        Tenant tenant,
        Guid elementId);

    Task MoveAppElementAsync(
        App app,
        Guid elementId);
}
