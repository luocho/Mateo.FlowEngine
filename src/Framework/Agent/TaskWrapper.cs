using System.Collections.Concurrent;
using System.Reflection;

namespace Framework;

public class TaskWrapper
{
    public TaskWrapper()
    {
        new ReflectionDispatcher(InternalSubscribe, InternalPublishAsync);
        new MessageDispatcher(InternalSubscribe, PublishAsync);
    }
    private readonly ConcurrentDictionary<string, Queue<ITask>> _taskQueues = new();
    private readonly ConcurrentDictionary<string, Queue<IDispatcher>> _typeQueues = new();
    public void Subscribe(string topic, ITask task)
    {
        if (!_taskQueues.TryGetValue(topic, out var queue))
        {
            queue = new Queue<ITask>();
            _taskQueues[topic] = queue;
        }
        queue.Enqueue(task);
    }
    private void InternalSubscribe(string topic, IDispatcher dispatcher)
    {
        if (!_typeQueues.TryGetValue(topic, out var queue))
        {
            queue = new Queue<IDispatcher>();
            _typeQueues[topic] = queue;
        }
        queue.Enqueue(dispatcher);
    }
    private async Task InternalPublishAsync(string topic, MateoMsg msg)
    {
        if (_typeQueues.TryGetValue(topic, out var queue))
        {
            foreach (var t in queue)
            {
                if (t != null)
                {
                    await t.HandleAsync(msg);
                }
            }
        }
    }

    public async Task PublishAsync(string topic, MateoMsg message)
    {
        //给所有订阅了该主题的任务发送消息
        if (_taskQueues.TryGetValue(topic, out var queue))
        {
            foreach (var task in queue)
            {
                // Here you can implement the logic to handle the message with the task
                var t = task.GetType();
                var fieldb = t.GetField("<inputParameter>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldb != null)
                {
                    fieldb.SetValue(task, message);
                }
                await task.ReceiveMessageAsync();
            }
        }
        //给所有订阅了该主题的分发器发送消息
        await InternalPublishAsync(topic, message);
    }
    public void Unsubscribe(string topic, ITask task)
    {
        if (_taskQueues.TryGetValue(topic, out var queue))
        {
            var newQueue = new Queue<ITask>(queue.Where(t => t != task));
            _taskQueues[topic] = newQueue;
        }
    }
}
