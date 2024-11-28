namespace CQ.AuthProvider.BusinessLogic.Identities;

public sealed record class Identity()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Email { get; init; }

    public required string Password { get; set; }
}
