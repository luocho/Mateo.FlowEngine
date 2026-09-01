using CommunicationFramework;

namespace Framework
{
    public class TaskWrapper
    {
        public TaskWrapper(ITask task)
        {
            new ReflectionDispatcher(this);
            new MessageDispatcher(this);

        }
        private  readonly Dictionary<Type, List<Func<object, Task>>> Handlers = [];
        private  readonly object SyncRoot = new();
        public  IDisposable Subscribe<T>(Func<T, Task> handler)
        {
            Func<object, Task> wrapper = message => handler((T)message);

            lock (SyncRoot)
            {
                if (!Handlers.TryGetValue(typeof(T), out var handlers))
                {
                    handlers = [];
                    Handlers[typeof(T)] = handlers;
                }

                handlers.Add(wrapper);
            }

            return new Subscription(() => Unsubscribe<T>(wrapper));
        }

        public  async Task PublishAsync<T>(T message)
        {
            Func<object, Task>[] handlers;

            lock (SyncRoot)
            {
                handlers = Handlers.TryGetValue(typeof(T), out var registered)
                    ? [.. registered]
                    : [];
            }

            foreach (var handler in handlers)
                await handler(message!);
        }

        private  void Unsubscribe<T>(Func<object, Task> handler)
        {
            lock (SyncRoot)
            {
                if (!Handlers.TryGetValue(typeof(T), out var handlers))
                    return;

                handlers.Remove(handler);
                if (handlers.Count == 0)
                    Handlers.Remove(typeof(T));
            }
        }

        private sealed class Subscription(Action unsubscribe) : IDisposable
        {
            private Action? _unsubscribe = unsubscribe;

            public void Dispose() => Interlocked.Exchange(ref _unsubscribe, null)?.Invoke();
        }

    }
}
