namespace Framework;

public interface IContext;
public interface IActionContext : IContext
{
    MateoMsg GetContext();
}
public interface IErrorContext : IContext
{
    MateoMsg GetContext();
}
public interface IDataContext : IContext
{
    MateoMsg GetContext();
}
public interface ILotContext : IContext
{
    MateoMsg GetContext();
}
public interface IEQPContext : IContext
{
    MateoMsg GetContext();
}
public interface IBatchContext : IContext
{
    MateoMsg GetContext();
}
public interface IConfig
{
    string TryGetValue(string key, string defaultValue="");
}

/// <summary>
/// 继承IAction接口必须的命名方式
/// X_X_Name 含义如下
/// A_P_Name A表示Action，P表示Process
/// A_S_Name A表示Action，S表示Send
/// A_W_Name A表示Action，W表示Wait
/// </summary>
public interface IAction
{
    Task RunAsync();
}

public interface IFlowLoader
{
    Task LoadFlowAsync();
}
public interface IFlowBuilder
{
    Task BuildFlowAsync();
}