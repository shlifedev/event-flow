using LD.Framework.EventFlow;

namespace EventFlow.Ref.Messages;

public struct TestUserChatMessage : IEventMessage
{
    public string ChatMessage { get; set; }
}
 