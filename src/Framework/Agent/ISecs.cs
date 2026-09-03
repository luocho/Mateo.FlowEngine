namespace Framework
{
    public interface ISecs
    {
        SecsWrapper wrapper { get; }
        Task RunAsync();
    }
}
