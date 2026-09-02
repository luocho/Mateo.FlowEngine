using Framework;

public class MateoMsg
{
    public override string ToString()
    {
        return $"{{" +
            $"{string.Join(",", _data
            .Where(p => !p.Key.Equals(Msg.Id))
            .Select(kv => $"\"{kv.Key}\": \"{kv.Value}\""))
            }}}";
    }
    public MateoMsg()
    {
        SetValue(Msg.Id, Guid.NewGuid().ToString());
    }
    public static MateoMsg CreateMsg()
    {
        return new MateoMsg();
    }
    public static MateoMsg CreateCommandMsg()
    {
        return new CommandMsg();
    }
    public static MateoMsg CreateCommandMsg(string command)
    {
        var msg = new CommandMsg();
        msg.SetValue(Msg.Command, command);
        return msg;
    }
    private Dictionary<string, string> _data { get; set; } = new Dictionary<string, string>();
    public string GetValue(string key)
    {
        if (_data.ContainsKey(key))
        {
            return _data[key];
        }
        return string.Empty;
    }
    public string TryGetValue(string key, string defaultValue = "")
    {
        if (_data.ContainsKey(key))
        {
            return _data[key];
        }
        return defaultValue;
    }
    public void SetValue(string key, string value)
    {
        if (_data.ContainsKey(key))
        {
            _data[key] = value;
        }
        else
        {
            _data.Add(key, value);
        }
    }
}

public class CommandMsg : MateoMsg
{
    public CommandMsg()
    {
        SetValue(Msg.MsgType, "C");

        SetValue(Msg.AssemblyName, "LibraryB");
        SetValue(Msg.TypeName, "OrderService");
        SetValue(Msg.MethodName, "ProcessAsync");
    }
}