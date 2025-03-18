namespace DefaultNamespace
{
    public class test
    {
        
    }
    
    public readonly struct TestUserChatMessage
    {
        public readonly string ChatMessage { get; }

        public TestUserChatMessage(string chatMessage)
        {
            ChatMessage = chatMessage;
        }
    }
}