using CommunicationFramework;

namespace Framework
{
    public class MessageDispatcher
    {
        public delegate Task MessageHandlerDelegate();
        public readonly MessageHandlerDelegate MessageHandler;
        public MessageDispatcher(TaskWrapper wrapper,ITask task)
        {
            wrapper.Subscribe<CallResult>(HandleMessageAsync);
            MessageHandler = task.ReceiveMessage;
        }

        private async Task HandleMessageAsync(CallResult message)
        {
            // Handle the message here
            await MessageHandler();
        }
    }
}
