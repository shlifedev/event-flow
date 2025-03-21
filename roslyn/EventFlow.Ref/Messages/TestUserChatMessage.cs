using LD.Framework.EventFlow;

namespace EventFlow.Ref.Messages;

public partial struct TestUserChatMessage : IEventMessage
{
    public string ChatMessage { get; set; }
}
 

 