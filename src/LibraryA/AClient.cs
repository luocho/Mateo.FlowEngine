using CommunicationFramework;
using Framework;

namespace LibraryA;

public class AClient : ITask
{
    public TaskWrapper wrapper { get; set; }
    public object[] inputParameter { get; set; }

    async Task ITask.Run()
    {
        //创建一个CallMessage对象，包含要调用的程序集名称、类型名称、方法名称和参数
        var message = new CallMessage
        {
            AssemblyName = "LibraryB",
            TypeName = "LibraryB.OrderService",
            MethodName = "ProcessAsync",
            Arguments = ["Water","123"]
        };
       await wrapper.PublishAsync(message);
    }

    async Task ITask.ReceiveMessage()
    {
        
    }
}
