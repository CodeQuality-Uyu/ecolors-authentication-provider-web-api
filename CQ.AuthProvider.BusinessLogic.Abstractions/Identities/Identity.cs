using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

public sealed record class Identity
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string Password { get; set; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public Identity()
    {
    }

    /// <summary>
    /// For new Identity
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public Identity(
        string email,
        string password)
    {
        Email = email;
        Password = password;
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public Identity(
        string id,
        string email,
        string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}
