using System.Collections.Concurrent;

namespace Framework;

public class TaskWrapper
{
    public TaskWrapper(ITask task)
    {
        new ReflectionDispatcher(InternalSubscribe);
        new MessageDispatcher(InternalSubscribe, task);
    }
    private readonly ConcurrentDictionary<string, Queue<ITask>> _taskQueues = new();
    private readonly ConcurrentDictionary<string, Queue<IDispatcher>> _typeQueues = new();
    /// <summary>
    /// Subscribes a task to a specific topic. When a message is published to that topic, the subscribed task will be executed.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="task"></param>
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
    private async Task InternalPublishAsync(string topic, MateoMsg message)
    {
        if (_typeQueues.TryGetValue(topic, out var queue))
        {
            foreach (var t in queue)
            {
                if (t != null)
                {
                  await t.HandleAsync(message);
                }
            }
        }
    }

    public async Task PublishAsync(string topic, MateoMsg message)
    {
        if (_taskQueues.TryGetValue(topic, out var queue))
        {
            foreach (var task in queue)
            {
                // Here you can implement the logic to handle the message with the task
                var t = task.GetType();
                if (t != null)
                    t.GetProperty(nameof(ITask.inputParameter)).SetValue(task, message);
                await task.Run();
            }
        }
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
