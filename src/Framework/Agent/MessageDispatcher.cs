using System.Reflection;

namespace Framework;

public class MessageDispatcher : IDispatcher
{
    public delegate Task PublishAsyncDelegate(string topic, MateoMsg msg);
    private PublishAsyncDelegate publishAsync;
    public MessageDispatcher(Action<string, IDispatcher> internalSubscribe, PublishAsyncDelegate publishAsync)
    {
        internalSubscribe(Msg.Reply, this);
        internalSubscribe(Msg.Event, this);
        this.publishAsync = publishAsync;
    }
    async Task IDispatcher.HandleAsync(MateoMsg msg)
    {
       await publishAsync(msg.GetValue(Msg.Topic,string.Empty), msg);

    }
}
