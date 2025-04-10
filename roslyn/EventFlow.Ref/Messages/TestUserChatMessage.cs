using LD.EventSystem;

namespace LD.EventSystem.Ref.Messages;


public partial struct TestUserChatMessage : IEventMessage
{
    public string ChatMessage { get; set; }
}


public partial struct TestUserChatMessage2 : IEventMessage
{
    public string ChatMessage { get; set; }
}


 