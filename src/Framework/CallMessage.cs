namespace CommunicationFramework;

// 这只是数据格式；可以按业务需要增删字段。
public sealed class CallMessage
{
    public Guid Id = Guid.NewGuid();
    public string AssemblyName = "";
    public string TypeName = "";
    public string MethodName = "";
    public object?[] Arguments = [];
}
