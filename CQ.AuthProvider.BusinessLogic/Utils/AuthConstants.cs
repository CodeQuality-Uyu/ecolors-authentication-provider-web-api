namespace CQ.AuthProvider.BusinessLogic.Utils;
public static class AuthConstants
{
    #region Role
    public static readonly Guid TENANT_OWNER_ROLE_ID = Guid.Parse("cf4a209a-8dbd-4dac-85d9-ed899424b49e");
    public static readonly Guid SEED_ROLE_ID = Guid.Parse("77f7ff91-a807-43ac-bc76-1b34c52c5345");
    public static readonly Guid AUTH_WEB_API_OWNER_ROLE_ID = Guid.Parse("780a89b1-9fd3-4cf6-b802-2882ebb3db92");
    public static readonly Guid APP_OWNER_ROLE_ID = Guid.Parse("4579a206-b6c7-4d58-9d36-c3e0923041b5");
    public static readonly Guid CLIENT_OWNER_ROLE_ID = Guid.Parse("01e55142-6b8c-4e7e-9d71-1e459d07796d");
    #endregion Role

    public static readonly Guid AUTH_WEB_API_APP_ID = Guid.Parse("f4ad89eb-6a0b-427a-8aef-b6bc736884dc");
    public static readonly Guid SEED_ACCOUNT_ID = Guid.Parse("0ee82ee9-f480-4b13-ad68-579dc83dfa0d");

    public static readonly Guid SEED_TENANT_ID = Guid.Parse("882a262c-e1a7-411d-a26e-40c61f3b810c");
}
