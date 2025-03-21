using LD.EventFlow.Attributes;
using LD.EventFlow.Ref.Messages;
using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Listeners;


public struct MsgA : IEventMessage
{
}
public struct MsgB : IEventMessage
{
}

[EventFlowListener]
public partial class ChatApplication : IEventListener<TestUserChatMessage>,
    IEventListener<MsgB>,
    IEventListener<MsgA>
{
    
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    }

    public void OnEvent(MsgB args)
    {
        throw new NotImplementedException();
    }

    public void OnEvent(MsgA args)
    {
        throw new NotImplementedException();
    }
}