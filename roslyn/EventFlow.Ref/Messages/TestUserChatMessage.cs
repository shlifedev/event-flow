using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Messages;

public interface QQMessage : IEventMessage
{
 
}
public partial struct TestUserChatMessage : QQMessage
{
    public string ChatMessage { get; set; }
}
 

 