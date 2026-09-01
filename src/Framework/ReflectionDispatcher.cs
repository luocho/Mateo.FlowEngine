using Framework;
using System.Reflection;

namespace CommunicationFramework;

public class ReflectionDispatcher
{
    public delegate Task MessageHandler(CallResult message);
    private readonly MessageHandler handler;
    public ReflectionDispatcher(TaskWrapper wrapper)
    {
        _subscription ??= wrapper.Subscribe<CallMessage>(HandleAsync);
        handler = new MessageHandler(wrapper.PublishAsync);

    }
    private IDisposable? _subscription;
    private async Task HandleAsync(CallMessage message)
    {
        CallResult result = new CallResult { Id = message.Id };

        try
        {
            var assembly = Assembly.Load(message.AssemblyName);
            var task = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Contains(typeof(ISecs)));


            var instance = task != null ? Activator.CreateInstance(task) : null;
            var returnValue = instance != null ? await ((ISecs)instance).RunAsync(message.Arguments) : null;

            result.Data = await GetReturnValueAsync(returnValue);
            result.Success = true;
        }
        catch (Exception exception)
        {
            result.Error = (exception as TargetInvocationException)?.InnerException?.Message
                           ?? exception.Message;
        }

        await handler.Invoke(result);
    }

    private static async Task<object?> GetReturnValueAsync(object? returnValue)
    {
        if (returnValue is not Task task)
            return returnValue;

        await task;
        return task.GetType().IsGenericType
            ? task.GetType().GetProperty("Result")?.GetValue(task)
            : null;
    }
}
