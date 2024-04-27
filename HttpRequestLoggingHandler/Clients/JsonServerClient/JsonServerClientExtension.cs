using HttpRequestLoggingHandler.Clients.Common;
using HttpRequestLoggingHandler.EFCore.DBEntities;

namespace Microsoft.Extensions.DependencyInjection;

public static class JsonServerClientExtension
{
    public static IServiceCollection AddJsonServerClient(this IServiceCollection services)
    {
        var dbContext = services.BuildServiceProvider().GetService<AppDbContext>();

        services
            .AddHttpClient(
                JsonServerClient.HttpClientName,
                client =>
                {
                    client.BaseAddress = new Uri(GetBaseUrl());
                })
            .AddHttpMessageHandler(
                () => new HttpClientLogHandler<JsonServerApiLog>(dbContext ?? throw new ArgumentNullException(nameof(dbContext))));

        services.AddScoped<IJsonServerClient, JsonServerClient>();

        return services;
    }

    private static string GetBaseUrl()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var baseUrl = environment == "Production" 
            ? JsonServerClient.BaseUrl_Prod 
            : JsonServerClient.BaseUrl_Dev;

        return baseUrl;
    }
}
