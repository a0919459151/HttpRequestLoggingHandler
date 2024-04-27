using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
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
    }
}
