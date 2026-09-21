namespace Framework;

public class ConfigService : IConfig
{
    private readonly Dictionary<string, string> _configurations;
    public ConfigService()
    {
        _configurations = new Dictionary<string, string>();
        _configurations[ConfigConst.URL] = "https://example.com";
        _configurations[ConfigConst.FilePath] = @"D:\demo.txt";
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

public class ContextService : ILotContext, IDataContext, IEQPContext, IBatchContext
{
    private MateoMsg _currentContext = new();
    public MateoMsg GetContext()
    {
        return _currentContext;
    }
}

public class FlowLoaderService(IConfig config) : FlowLoader
{
    public void LoadFlow()
    {
        config.TryGetValue("FlowPath",string.Empty);
    }
}