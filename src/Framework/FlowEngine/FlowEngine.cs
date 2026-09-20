using Microsoft.Extensions.DependencyInjection;

namespace Framework
{
    public class FlowEngine(IServiceProvider provider)
    {
        public async Task ExecuteFlow(string flowName)
        {
            var action = provider.GetKeyedService(typeof(IAction), flowName);
            var actionInstance = action as IAction;
            if (actionInstance != null) { 
                await actionInstance.RunAsync(); 
            }
            // Implementation for executing a flow based on the flowName
            Console.WriteLine($"Executing flow: {flowName}");
            // Add logic to execute the flow here
        }
    }
}
