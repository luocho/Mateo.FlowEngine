using Microsoft.Extensions.DependencyInjection;

namespace Framework
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection Init(this IServiceCollection services)
        {
            services.InitLogger();
            services.InitContextTypes();
            services.InitActionTypes();
            services.AddSingleton<FlowEngine>();
            services.AddSingleton<IConfig, ConfigService>();
            services.AddSingleton<IFlowLoader, FlowLoaderService>();
            services.AddSingleton<IFlowBuilder, FlowBuilderService>();
            return services;
        }
    }
}
