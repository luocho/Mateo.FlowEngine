namespace CommunicationFramework;

public sealed class CallResult
{
    public Guid Id;
    public bool Success;
    public object? Data;
    public string? Error;
}
