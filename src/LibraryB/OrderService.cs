using Framework;

namespace LibraryB;

public sealed class OrderService : ISecs
{
    public SecsWrapper wrapper { get; }
    public async Task RunAsync()
    {
        await Task.Delay(1000);
    }
}
