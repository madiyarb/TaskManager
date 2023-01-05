namespace ExtendedHttpClient.Interfaces;

public interface IUseExtendedHttpClient<T> where T : class
{
    public ExtendedHttpClient<T> ExtendedHttpClient { get; set; }
}
