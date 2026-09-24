using Framework;
using System.Collections.Concurrent;

public class MateoMsg
{
    public override string ToString()
    {
        return $"{{" +
            $"{string.Join(",", _data
            .Where(p => !p.Key.Equals(Msg.Id))
            .Select(kv => $"\"{kv.Key}\": \"{kv.Value}\""))}}}";
    }
    public MateoMsg()
    {
        SetValue(Msg.Id, Guid.NewGuid().ToString());
    }

    private ConcurrentDictionary<string, string> _data { get; set; } = new ConcurrentDictionary<string, string>();
    private ConcurrentDictionary<string, object> _object { get; set; } = new ConcurrentDictionary<string, object>();
    public object GetObjValue(string key)
    {
        return _object[key];
    }
    public string GetValue(string key, string defaultValue = "")
    {
        if (_data.ContainsKey(key))
        {
            return _data[key];
        }
        return defaultValue;
    }
    public void SetValue(string key, object value, bool isObject = false)
    {
        if (isObject)
        {
            _object[key] = value;
        }
        else if (value is string data)
        {
            _data[key] = data;
        }
        else
        {
            throw new InvalidDataException("value must be object or string");
        }

    }
}

public static class MateoMsgHelper
{
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
        var newMsg = new CommandMsg();
        newMsg.SetValue(Msg.Command, command);
        return newMsg;
    }
    public static MateoMsg CreateReplyMsg()
    {
        return new ReplyMsg();
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
public class ReplyMsg : MateoMsg
{
    public ReplyMsg()
    {
        SetValue(Msg.MsgType, "R");
    }
}