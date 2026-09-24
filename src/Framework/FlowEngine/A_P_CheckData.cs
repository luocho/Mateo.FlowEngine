using Serilog;

namespace Framework;

public class A_P_CheckData(ILogger logger) : IAction
{
    public async Task RunAsync()
    {
        logger.Information("S\t:Check Completed result:OK");
        await Task.CompletedTask;
    }
}
