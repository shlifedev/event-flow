using System.Collections.Generic;
using LD.EventSystem;
using UnityEngine;


public interface IPacket
{
    public int Id { get; } // some pck headers..
    public int PacketType { get; } // pck types.. }
}


public struct ChatPacket : IPacket
{
    public int Id { get; }

    public int PacketType => 1000;

    public int sender;
    public string message;

}
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

public class NetworkManager : MonoBehaviour
{
    public Queue<IPacket> receivedPackets = new();


    void OnEnable()
    {
        EventFlow.Register(this);
    }
    void Update()
    {
        bool has = receivedPackets.TryPeek(out var result);
        if (has)
        {
            Debug.Log("dispatch start");
            Dispatch(result);
            receivedPackets.Dequeue();

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var cp = new ChatPacket();
            cp.message = "안녕안";
            cp.sender = Random.Range(1, 10);
            this.receivedPackets.Enqueue(cp);
        }

    }


    void Dispatch(IPacket packet)
    {
        switch (packet.PacketType)
        {
            case 1000: // opcode = chat message
                var chatPacket = (ChatPacket)packet;
                var msg = new NetworkChatReceivedMessage(chatPacket);
                EventFlow.Broadcast(msg);
                break;
        }

    }

}