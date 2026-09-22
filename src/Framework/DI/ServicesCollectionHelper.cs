using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace Framework;

public static class ServicesCollectionHelper
{
    public static void InitLogger(this IServiceCollection services)
    {
        const string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}";
        string logDirectory = Path.Combine(AppContext.BaseDirectory, "log");
        Directory.CreateDirectory(logDirectory);
        string logFilePath = Path.Combine(logDirectory, "log-.txt");
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .Enrich.FromLogContext()
           .WriteTo.File(
                logFilePath,
                outputTemplate: outputTemplate,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30
            ).CreateLogger();
        services.AddSingleton(Log.Logger);
    }
    public static void InitActionTypes(this IServiceCollection services)
    {
        var actionTypes = typeof(IAction).Assembly.DefinedTypes
                .Where(t => t is { IsClass: true, IsAbstract: false } &&
                typeof(IAction).IsAssignableFrom(t)
                );

        foreach (var actionType in actionTypes)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.KeyedTransient(
                    typeof(IAction),
                    actionType.Name,
                    actionType.AsType()));
        }

    }
    public static void InitContextTypes(this IServiceCollection services)
    {
        var contextTypes = typeof(IContext).Assembly.DefinedTypes
                .Where(t => t is { IsInterface: true } &&
                typeof(IContext).IsAssignableFrom(t)
                );
        foreach (var contextType in contextTypes)
        {
            var serviceTypes = contextType.Assembly.DefinedTypes
            .Where(t => t is { IsClass: true, IsAbstract: false, } &&
            contextType.IsAssignableFrom(t)
            );
            if (serviceTypes.Any())
            {
                foreach (var serviceType in serviceTypes)
                {
                    services.TryAddEnumerable(
                        ServiceDescriptor.Singleton(contextType, serviceType)
                        );
                }
            }
        }
    }
}
