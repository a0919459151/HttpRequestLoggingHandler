using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new Exception("Entry assembly not found");
        var types = assembly.GetTypes()
            .Where(type => type.IsClass
                && !string.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains("Services")
                && !string.IsNullOrEmpty(type.Name) && type.Name.EndsWith("Service"))
            .ToList();

        foreach (var type in types)
        {
            var interfaceType = type.GetInterface($"I{type.Name}");
            if (interfaceType == null) continue;
            services.AddScoped(interfaceType, type);
        }

        return services;
    }
}
