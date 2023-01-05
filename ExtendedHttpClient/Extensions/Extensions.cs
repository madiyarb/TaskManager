using ExtendedHttpClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendedHttpClient.Extensions;

public static class Extensions
{
    private static bool _isInitialized = false;
    public static IServiceCollection AddExtendedHttpClient(this IServiceCollection services, Type? extendedHttpClient = null)
    {
        if (_isInitialized) throw new InvalidOperationException("ExtendedHttpClient is initialized earlier");
        services.AddHttpClient();
        services.AddSingleton(extendedHttpClient != null
            ? new ExtendedHttpClientOptions(extendedHttpClient)
            : new ExtendedHttpClientOptions());
        _isInitialized = true;
        return services;
    }

    public static IServiceCollection AddServiceWithExtendedHttpClient<TClass>(this IServiceCollection services, string uri)
        where TClass : class, IUseExtendedHttpClient<TClass>
    {
        services.AddServiceWithExtendedHttpClient<TClass>(httpOptions => httpOptions.BaseAddress = new Uri(uri));
        return services;
    }

    public static IServiceCollection AddServiceWithExtendedHttpClient<TClass>(this IServiceCollection services, Action<HttpClient> httpOptions)
        where TClass : class, IUseExtendedHttpClient<TClass>
    {
        if (!_isInitialized) throw new InvalidOperationException("ExtendedHttpClient is not initialized, use method: AddExtendedHttpClient");
        services.AddSingleton(provider => new HttpClientOptions<TClass>(httpOptions));
        var options = services.BuildServiceProvider().GetRequiredService<ExtendedHttpClientOptions>();
        Type[] typesArgs = { typeof(TClass) };
        services.AddTransient(typeof(ExtendedHttpClient<TClass>), options.ExtendedHttpClientType.MakeGenericType(typesArgs));
        services.AddTransient<TClass>();

        return services;
    }

    public static IServiceCollection AddServiceWithExtendedHttpClient<TClass, TImplementation>(this IServiceCollection services, string uri)
        where TClass : class, IUseExtendedHttpClient<TClass>
        where TImplementation : class, TClass
    {
        services.AddServiceWithExtendedHttpClient<TClass, TImplementation>(httpOptions => httpOptions.BaseAddress = new Uri(uri));
        return services;
    }

    public static IServiceCollection AddServiceWithExtendedHttpClient<TClass, TImplementation>(this IServiceCollection services, Action<HttpClient> httpOptions)
        where TClass : class, IUseExtendedHttpClient<TClass>
        where TImplementation : class, TClass
    {
        if (!_isInitialized) throw new InvalidOperationException("ExtendedHttpClient is not initialized, use method: AddExtendedHttpClient");
        services.AddSingleton(provider => new HttpClientOptions<TClass>(httpOptions));
        var options = services.BuildServiceProvider().GetRequiredService<ExtendedHttpClientOptions>();
        Type[] typesArgs = { typeof(TClass) };
        var type = options.ExtendedHttpClientType.MakeGenericType(typesArgs);
        services.AddTransient(typeof(ExtendedHttpClient<TClass>), type);
        services.AddTransient<TClass, TImplementation>();

        return services;
    }
}
