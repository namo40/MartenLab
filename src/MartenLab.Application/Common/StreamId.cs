namespace MartenLab.Application.Common;

public static class StreamId<TState>
    where TState : class
{
    private static readonly string Prefix = typeof(TState).Name.EndsWith("State")
        ? typeof(TState).Name[..^5]
        : typeof(TState).Name;

    public static string GetStreamId(string id)
    {
        return $"{Prefix}/{id}";
    }
}