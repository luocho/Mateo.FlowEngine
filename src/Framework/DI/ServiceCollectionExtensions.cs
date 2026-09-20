using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Init(this IServiceCollection services)
        {
            services.AddSingleton<FlowEngine>();
            services.AddSingleton<IConfig, ConfigService>();
            var actionTypes = typeof(IAction).Assembly.DefinedTypes
                .Where(t => t is { IsClass: true, IsAbstract: false } &&
                typeof(IAction).IsAssignableFrom(t)
                );
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
            foreach (var actionType in actionTypes)
            {
                services.TryAddEnumerable(
                    ServiceDescriptor.KeyedTransient(
                        typeof(IAction),
                        actionType.Name,
                        actionType.AsType()));
            }
            return services;
        }
    }
}
