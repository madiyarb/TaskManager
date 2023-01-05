namespace ExtendedHttpClient;

public class HttpClientOptions<T>
{
    public Action<HttpClient> Configure { get; }

    public HttpClientOptions(Action<HttpClient> options)
    {
        Configure = options;
    }
}
