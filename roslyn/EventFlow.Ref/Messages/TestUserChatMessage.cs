using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Messages;

public partial struct TestUserChatMessage : IEventMessage
{
    public string ChatMessage { get; set; }
}
 

 