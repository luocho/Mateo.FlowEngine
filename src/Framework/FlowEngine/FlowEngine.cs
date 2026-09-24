using Google.Protobuf.Collections;
using Serilog;
using System.Collections.Concurrent;

namespace Framework;

public class FlowEngine(
    IFlowBuilder builder, 
    IDataContext dataContext, 
    FlowRunner flowRunner, 
    IActionContext actionContext,
    ILogger logger
    )
{
    private ConcurrentQueue<string> runStepQueue = new ConcurrentQueue<string>();
    private Flow currentFlow=new Flow();
    private delegate Task StartRun();
    private StartRun? startRun;
    public async Task RunAsync()
    {
        await builder.BuildFlowAsync();
        startRun += ExcuteAsync;
        var obj = dataContext.GetContext().GetObjValue(ContextConst.FlowObject);
        if (obj is Flow flow)
        {
            currentFlow = flow;
            var currentStep = FindStepByID(1);
            runStepQueue.Enqueue(currentStep.RunType);
            await ExcuteAsync();
        }
    }
    private void Router(string runType)
    {
        var currentStep = FindStepByRunType(runType);
        var actionResult = actionContext.GetContext().GetValue(currentStep.RunType);
        logger.Information("E\t:{0} Result:{1}", runType, actionResult);
        List<NextStep> nextStepList = MatchNextStep(currentStep.NextSteps, actionResult);
        foreach (var step in nextStepList)
        {
            runStepQueue.Enqueue(step.NextStepRunType);
        }
        if (!runStepQueue.IsEmpty)startRun?.Invoke();
    }
    private async Task ExcuteAsync()
    {
        runStepQueue.TryDequeue(out var runnType);
        try
        {
            logger.Information("S\t:{0}", runnType);
            if (runnType is not null)
            {
                await flowRunner.ExecuteFlow(runnType);
                Router(runnType);
            }
        }
        catch (Exception ex)
        {
            logger.Error("Flow {0}:{1}", runnType,ex.Message);
        }
        
    }
    private FlowStep FindStepByID(int id) => currentFlow.Steps.First(p => p.Id == id);
    private FlowStep FindStepByRunType(string runType) => currentFlow.Steps.First(p => p.RunType.Equals(runType));
    private List<NextStep> MatchNextStep(RepeatedField<NextStep> nextSteps,string actionResult)
    {
        var result = nextSteps.Where(p=>p.NextStepConditions.Equals(actionResult)).ToList();
        return result;
    }
}
