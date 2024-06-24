using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

public sealed record class Identity
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string Password { get; set; } = null!;

    public Identity()
    {
    }

    public Identity(
        string email,
        string password)
    {
        Email = email;
        Password = password;
    }
}
