using CommunicationFramework;

namespace Framework
{
    public interface ITask
    {
        TaskWrapper wrapper { get; }
        object[] inputParameter { get; }
        Task Run();
        Task ReceiveMessage();
    }
}
