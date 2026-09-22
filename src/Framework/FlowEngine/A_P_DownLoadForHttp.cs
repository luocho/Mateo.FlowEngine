namespace Framework;

public sealed class A_P_DownLoadForHttp(IConfig _config, IDataContext _contextService,IErrorContext errorContext) : IAction
{
    //下载文件-》解析文件-》处理文件-》存储文件
    //依赖注入，任务取消，
    public async Task RunAsync()
    {
        try
        {
            string url = _config.TryGetValue(ConfigConst.URL);
            string fileContent = await DownloadFileAsync(url);
            _contextService.GetContext().SetValue(ContextConst.DownloadContent, fileContent);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            errorContext.GetContext().SetValue(ErrorConst.ErrorMsg,ex.Message);
        }
    }

    private async Task<string> DownloadFileAsync(string url)
    {
        Task.Delay(1000).Wait();
        return await Task.FromResult("Hello world!\n" + url);
    }
}
