namespace Framework;

public sealed class A_S_SaveFile(IDataContext context, IConfig config) : IAction
{
    public Task RunAsync()
    {
        var data = context.GetContext().GetValue(ContextConst.HandleData);
        var filePath = config.TryGetValue(ConfigConst.FilePath);
        File.WriteAllTextAsync(filePath, data);
        return Task.CompletedTask;
    }
}

