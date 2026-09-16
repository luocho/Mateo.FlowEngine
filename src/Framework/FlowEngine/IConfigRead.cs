using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.FlowEngine
{
    public interface IConfigRead
    {
        string TryGetConfigValue(string key, string defaultValue);
    }
}
