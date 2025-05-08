using LD.EventSystem.Ref.Messages;
using LD.EventSystem;
using LD.EventSystem.Attributes;

namespace LD.EventSystem.Ref.Listeners;



[EventFlowListener]
public partial class ChatApplication : IEventListener<TestUserChatMessage>,IEventListener<TestUserChatMessage2>
{
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    }

    public void OnEvent(TestUserChatMessage2 args)
    {

    }
}
