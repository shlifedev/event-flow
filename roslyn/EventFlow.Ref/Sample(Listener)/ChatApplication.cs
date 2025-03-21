using EventFlow.Ref.Messages;
using LD.EventFlow.Attributes;
using LD.Framework.EventFlow;

namespace EventFlow.Ref.Listeners;


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