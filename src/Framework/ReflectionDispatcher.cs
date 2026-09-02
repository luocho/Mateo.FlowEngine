using System.Reflection;

namespace Framework;

public class ReflectionDispatcher : IDispatcher
{
    public ReflectionDispatcher(Action<string, IDispatcher> internalSubscribe)
    {
        internalSubscribe(Msg.Command, this);
    }
    async Task IDispatcher.HandleAsync(MateoMsg msg)
    {
        try
        {
            var assembly = Assembly.Load(msg.GetValue("AssemblyName"));
            var task = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Contains(typeof(ISecs)) && t.Name == msg.GetValue(Msg.TypeName));
            var instance = task != null ? Activator.CreateInstance(task) : null;
            if (task != null && instance != null)
            {
                var field = task.GetField("<wrapper>k__BackingField",
        BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(instance, new SecsWrapper());
            }
            if (instance != null)
            {
                await ((ISecs)instance).RunAsync();
            }
        }
        catch (Exception exception)
        {
            string errorMessage = exception.Message;
            throw;
        }

    }
}
