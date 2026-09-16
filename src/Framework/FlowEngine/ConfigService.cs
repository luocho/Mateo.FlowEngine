using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.FlowEngine
{
    //程序启动做的事情
    public class ConfigService : IConfigRead
    {
        private readonly Dictionary<string, string> _configurations;
        public ConfigService()
        {
            _configurations = new Dictionary<string, string>();
            _configurations["Url"] = "https://example.com";
        }
        public string TryGetConfigValue(string key, string defaultValue="")
        {
            // 实现获取配置值的逻辑
            if (_configurations.TryGetValue(key, out string value))
            {
                return value;
            }
            return defaultValue;
        }
    }
}
