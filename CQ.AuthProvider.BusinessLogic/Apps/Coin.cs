namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record Coin()
{
  public Guid Id { get; init; } = Guid.NewGuid();

  public string Name { get; init; } = null!;

  public string? Key { get; init; }

  public string Locale { get; init; }
}
