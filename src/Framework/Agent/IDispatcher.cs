namespace Framework
{
    public interface IDispatcher
    {
        Task HandleAsync(MateoMsg msg);
    }
}
