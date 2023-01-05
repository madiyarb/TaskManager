namespace ExtendedHttpClient;

public class ExtendedHttpClientOptions
{
    public Type ExtendedHttpClientType { get; init; }

    public ExtendedHttpClientOptions()
    {
        ExtendedHttpClientType = typeof(ExtendedHttpClient<>);
    }

    public ExtendedHttpClientOptions(Type extendedHttpClientType) : this()
    {
        if (extendedHttpClientType.BaseType!.ToString() != typeof(ExtendedHttpClient<>).ToString())
            throw new InvalidCastException($"{extendedHttpClientType} is not inherited ExtendedHttpClient<T>");
        ExtendedHttpClientType = extendedHttpClientType;
    }
}


