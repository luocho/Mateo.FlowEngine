using Serilog;

namespace Framework;

internal class A_P_Error(ILogger logger, IErrorContext errorContext) : IAction
{
    public async Task RunAsync()
    {
        logger.Error($"{errorContext.GetContext().GetValue(ErrorConst.ErrorMsg)}");
        await Task.CompletedTask;
    }
}
