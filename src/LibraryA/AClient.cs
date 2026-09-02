using Framework;

namespace LibraryA;

public class AClient : ITask
{
    public TaskWrapper wrapper { get; set; }
    public MateoMsg inputParameter { get; set; }

    async Task ITask.Run()
    {
        //创建一个CallMessage对象，包含要调用的程序集名称、类型名称、方法名称和参数
        var msg = MateoMsg.CreateCommandMsg(Command.Order);
        var t = msg.ToString();
        await wrapper.PublishAsync(Msg.Command, msg);
        //await wrapper.Subscribe<string>();
    }
    async Task ITask.ReceiveMessage()
    {
        Console.WriteLine("Message received.");
    }
}
