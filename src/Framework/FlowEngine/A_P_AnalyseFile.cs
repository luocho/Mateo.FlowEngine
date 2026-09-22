namespace Framework;

public sealed class A_P_AnalyseFile(IDataContext _contextService) : IAction
{
    public async Task RunAsync()
    {
        var content = _contextService.GetContext();
        var msg = _contextService.GetContext();
        string data = await HandleData(msg.GetValue(ContextConst.DownloadContent));
        msg.SetValue(ContextConst.HandleData, data);
    }

    private async Task<string> HandleData(string msg)
    {
        Task.Delay(1000).Wait();
        return await Task.FromResult($"{msg.ToUpper()}");
    }
}
