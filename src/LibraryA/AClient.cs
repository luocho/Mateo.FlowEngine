using Framework;

namespace LibraryA;

public class AClient : ITask
{
    public TaskWrapper wrapper { get; }
    public MateoMsg inputParameter { get; }

    async Task ITask.Run()
    {
        var msg = MateoMsg.CreateCommandMsg(Command.Order);
        var t = msg.ToString();
        await wrapper.PublishAsync(Msg.Command, msg);
    }
    async Task ITask.ReceiveMessageAsync()
    {
    }
}
public class BClient : ITask
{
    public TaskWrapper wrapper { get; }
    public MateoMsg inputParameter { get; }

    async Task ITask.Run()
    {
        wrapper.Subscribe("DiaoMao", this);
    }
    async Task ITask.ReceiveMessageAsync()
    {
        Console.WriteLine("Message received.");
    }
}