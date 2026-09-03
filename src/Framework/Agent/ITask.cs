namespace Framework
{
    public interface ITask
    {
        TaskWrapper wrapper { get; }
        MateoMsg inputParameter { get; }
        Task Run();
        Task ReceiveMessageAsync();
    }
}
