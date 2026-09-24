namespace Framework;

public sealed class A_S_SaveFile(
    IActionContext actionContext,
    IErrorContext errorContext,
    IDataContext dataContext,
    IConfig config
    ) : IAction
{
    public async Task RunAsync()
    {
        try
        {
            var data = dataContext.GetContext().GetValue(ContextConst.HandleData);
            var filePath = config.TryGetValue(ConfigConst.FilePath);
            await File.WriteAllTextAsync(filePath, data);
            actionContext.GetContext().SetValue(nameof(A_S_SaveFile), ActionResult.OK);
        }
        catch (Exception ex)
        {
            errorContext.GetContext().SetValue(ErrorConst.ErrorMsg, ex.Message);
            actionContext.GetContext().SetValue(nameof(A_P_DownLoadForHttp), ActionResult.NO);
        }

    }
}

