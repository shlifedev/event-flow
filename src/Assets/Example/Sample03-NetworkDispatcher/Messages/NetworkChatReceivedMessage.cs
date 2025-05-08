using LD.EventSystem;

public struct NetworkChatReceivedMessage : IEventMessage
{
    public string Message { get; }
    public string Sender { get; }
    public NetworkChatReceivedMessage(ChatPacket packet)
    {
        Message = packet.message;
        Sender = "Player" + packet.sender;
    }
}