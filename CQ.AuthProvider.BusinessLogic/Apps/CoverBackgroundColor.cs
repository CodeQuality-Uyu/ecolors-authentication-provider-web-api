namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record CoverBackgroundColor()
{
    public List<string> Colors { get; init; } = [];

    public string Config { get; init; } = null!;
}
