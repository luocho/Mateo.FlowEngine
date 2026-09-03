namespace Framework
{
    public class SecsWrapper
    {
        public delegate Task InternalSubscribeDelegate(string topic, MateoMsg msg);
        private InternalSubscribeDelegate internalSubscribeDelegate;

        public SecsWrapper(InternalSubscribeDelegate internalSubscribeDelegate)
        {
            this.internalSubscribeDelegate = internalSubscribeDelegate;
        }
        public async Task SendMsgAsync(string topic,MateoMsg msg)
        {
            await internalSubscribeDelegate(topic, msg);
        }
    }
}