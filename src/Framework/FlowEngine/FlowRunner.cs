using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Framework;

public class FlowRunner(IServiceProvider provider, ILogger logger)
{
    public async Task ExecuteFlow(string flowName)
    {
        try
        {
            var action = provider.GetKeyedService(typeof(IAction), flowName);
            var actionInstance = action as IAction;
            if (actionInstance != null)
            {
                await actionInstance.RunAsync();
            }
            else
            {
                throw new ArgumentNullException($"{flowName} not exist");
            }
        }
        catch (Exception ex)
        {
            logger.Error("FlowName={0} Error: {1}",flowName,ex.Message);
        }
    }
}
