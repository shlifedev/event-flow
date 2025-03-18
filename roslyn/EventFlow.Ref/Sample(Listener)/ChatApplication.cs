using EventFlow.Ref.Messages;
using LD.Framework.EventFlow;

namespace EventFlow.Ref.Listeners;


public class ChatApplication : IEventListener<TestUserChatMessage>
{
    
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    }
}