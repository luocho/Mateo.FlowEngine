using Framework;

namespace LibraryB;

public sealed class OrderService : ISecs
{
    public SecsWrapper wrapper { get; }
    public async Task RunAsync()
    {
        await Task.Delay(1000);
        MateoMsg replyMsg = MateoMsg.CreateReplyMsg();
        replyMsg.SetValue(Msg.Topic, "DiaoMao");
        replyMsg.SetValue("Mateo","Hello World!");
        await wrapper.SendMsgAsync(Msg.Reply, replyMsg);
    }
}
