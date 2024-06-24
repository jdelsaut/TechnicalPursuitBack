using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Refit;

namespace TechnicalPursuitApi.Sdk;

public static class DependencyInjection
{
    public static IServiceCollection AddTechnicalPursuitApiSdk(
        this IServiceCollection services,
        Uri technicalPursuitApiUri)
    {
#pragma warning disable S4830 // Desactivation des certificats le temps d'update tous les certifcats - Server certificates should be verified during SSL/TLS connections
        services.AddRefitClient<ITechnicalPursuitApi>()
            .ConfigureHttpClient(c => c.BaseAddress = technicalPursuitApiUri)
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true })
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.ConfigureRetry();

                // options.CircuitBreaker.ConfigureCircuitBreaker();
                // options.RateLimiter.ConfigureRateLimiter();
                // options.TotalRequestTimeout.ConfigureTotalRequestTimeout();
            });
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections

        return services;
    }

    public static void ConfigureRetry(this HttpRetryStrategyOptions options)
    {
        options.MaxRetryAttempts = 4;
        options.Delay = TimeSpan.FromSeconds(5);
        options.OnRetry = _ => default;
    }

    public static void ConfigureCircuitBreaker(this HttpCircuitBreakerStrategyOptions options)
    {
        options.BreakDuration = TimeSpan.FromSeconds(30);
    }

    public static void ConfigureRateLimiter(this HttpRateLimiterStrategyOptions options)
    {
        options.RateLimiter = _ => default;
    }

    public static void ConfigureTotalRequestTimeout(this HttpTimeoutStrategyOptions options)
    {
        options.Timeout = TimeSpan.FromSeconds(60);
        options.OnTimeout = _ => default;
    }
}