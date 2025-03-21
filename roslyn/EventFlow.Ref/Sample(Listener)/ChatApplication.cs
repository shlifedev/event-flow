using LD.EventFlow.Attributes;
using LD.EventFlow.Ref.Messages;
using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Listeners;


 
[EventFlowListener]
public partial class ChatApplication : IEventListener<TestUserChatMessage>
{
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    } 
}
