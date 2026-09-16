namespace Framework.FlowEngine
{
    public class DownLoadForHttp : IAction
    {
        //下载文件-》解析文件-》处理文件-》存储文件
        //依赖注入，任务取消，
        //注入IConfigRead接口的实例
        private readonly IConfigRead _config;
        public DownLoadForHttp(IConfigRead config)
        {
            _config = config;
        }
        public async Task RunAsync()
        {
            // 读取配置的逻辑
            string url = _config.TryGetConfigValue("Url", "https://example.com");
            await DownloadFileAsync(url);
            await Task.CompletedTask;
        }

        private async Task DownloadFileAsync(string url)
        {
            // 使用HttpClient下载文件的逻辑
        }
    }
}
