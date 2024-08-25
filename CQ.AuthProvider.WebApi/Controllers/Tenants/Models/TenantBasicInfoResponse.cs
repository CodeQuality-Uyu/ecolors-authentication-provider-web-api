namespace CQ.AuthProvider.WebApi.Controllers.Tenants.Models;

public readonly struct TenantBasicInfoResponse
{
    public string Id { get; init; }

    public string Name { get; init; }

    public AccountBasicInfoResponse Owner { get; init; }
}

public readonly struct AccountBasicInfoResponse
{
    public string Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string FullName { get; init; }

    public string? ProfilePictureId { get; init; }

    public string Email { get; init; }
}