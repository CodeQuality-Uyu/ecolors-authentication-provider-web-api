namespace CQ.AuthProvider.DataAccess.EfCore.Apps;

public sealed record class CoverBackgroundColorEfCore()
{
    public List<string> Colors { get; init; } = [];

    public string Config { get; init; } = null!;
}
