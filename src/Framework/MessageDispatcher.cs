namespace Framework;

public class MessageDispatcher : IDispatcher
{
    public delegate Task MessageHandlerDelegate();
    public readonly MessageHandlerDelegate MessageHandler;
    public MessageDispatcher(Action<string, IDispatcher> internalSubscribe, ITask task)
    {
        internalSubscribe(Msg.Reply, this);
        internalSubscribe(Msg.Event, this);
        MessageHandler = task.ReceiveMessage;
    }
    async Task IDispatcher.HandleAsync(MateoMsg msg)
    {
        // Handle the message here
        await MessageHandler();
    }
}
