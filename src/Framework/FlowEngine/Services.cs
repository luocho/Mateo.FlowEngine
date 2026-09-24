using Google.Protobuf;
using Serilog;

namespace Framework;

public class ConfigService : IConfig
{
    private readonly Dictionary<string, string> _configurations;
    public ConfigService()
    {
        _configurations = new Dictionary<string, string>();
        _configurations[ConfigConst.URL] = "https://example.com";
        _configurations[ConfigConst.FilePath] = @"D:\demo.txt";
        _configurations[ConfigConst.FlowPath] = @"D:\mateoflow.mf";
    }
    public string TryGetValue(string key, string defaultValue = "")
    {
        // 实现获取配置值的逻辑
        if (_configurations.TryGetValue(key, out string value))
        {
            return value;
        }
        return defaultValue;
    }
}

public class ContextService : ILotContext, IDataContext, IEQPContext, IBatchContext, IErrorContext, IActionContext
{
    private MateoMsg _currentContext = new();
    public MateoMsg GetContext()
    {
        return _currentContext;
    }
}

public class FlowLoaderService(ILogger logger, IConfig config, IDataContext data) : IFlowLoader
{
    public async Task LoadFlowAsync()
    {
        logger.Information($"S\t:Read Flow");
        string flowPath = config.TryGetValue("FlowPath", string.Empty);

        await write(flowPath);
        logger.Information($"P\t:FlowPath={flowPath}");
        byte[] flowData = await File.ReadAllBytesAsync(flowPath);
        Flow flow = Flow.Parser.ParseFrom(flowData);
        data.GetContext().SetValue(ContextConst.FlowObject, flow, true);
        logger.Information($"E\t:Read Flow Completed");
    }

    private async Task write(string flowPath)
    {
        #region Demo
        var flows = new Flow();
        var currentStep = new FlowStep
        {
            Id = 1,
            Name = "A_P_DownLoadForHttp",
            RunType = "A_P_DownLoadForHttp",
            StepType = FlowStepType.Action
        };
        var nextSetp = new NextStep
        {
            NextStepId = 2,
            NextStepConditions = "OK",
            NextStepRunType= "A_P_AnalyseFile"
        };
        currentStep.NextSteps.Add(nextSetp);
        nextSetp = new NextStep
        {
            NextStepId = 4,
            NextStepConditions = "NO",
            NextStepRunType = "A_P_Error"
        };
        currentStep.NextSteps.Add(nextSetp);
        flows.Steps.Add(currentStep);

        currentStep = new FlowStep
        {
            Id = 2,
            Name = "A_P_AnalyseFile",
            RunType = "A_P_AnalyseFile",
            StepType = FlowStepType.Action
        };
        nextSetp = new NextStep
        {
            NextStepId = 3,
            NextStepConditions = "OK",
            NextStepRunType = "A_S_SaveFile"
        };
        currentStep.NextSteps.Add(nextSetp);
        flows.Steps.Add(currentStep);

        currentStep = new FlowStep
        {
            Id = 3,
            Name = "A_S_SaveFile",
            RunType = "A_S_SaveFile",
            StepType = FlowStepType.Action
        };
        nextSetp = new NextStep
        {
            NextStepId = 4,
            NextStepConditions = "NO",
            NextStepRunType = "A_P_Error"
        };
        currentStep.NextSteps.Add(nextSetp);
        flows.Steps.Add(currentStep);

        currentStep = new FlowStep
        {
            Id = 4,
            Name = "A_P_Error",
            RunType = "A_P_Error",
            StepType = FlowStepType.Action
        };
        flows.Steps.Add(currentStep);
        #endregion
        await File.WriteAllBytesAsync(flowPath, flows.ToByteArray());
    }
}
public class FlowBuilderService(ILogger logger, IFlowLoader flowLoader) : IFlowBuilder
{
    public async Task BuildFlowAsync()
    {
        logger.Information($"S\t:Build Flow");
        await flowLoader.LoadFlowAsync();
    }
}