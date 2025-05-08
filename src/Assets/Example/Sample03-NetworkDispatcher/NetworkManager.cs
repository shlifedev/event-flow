using System;
using System.Collections.Generic;
using LD.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;


public class NetworkManager : MonoBehaviour
{
    public Queue<IPacket> receivedPackets = new();

    public static NetworkManager Instance;

    private void Awake()
    {
        Instance ??= GetComponent<NetworkManager>();
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
    }


    /*
     * TODO
     *
     */
    void Dispatch(IPacket packet)
    {
        switch (packet.PacketType)
        {
            case 1000: // opcode = chat message
                var chatPacket = (ChatPacket)packet;
                var msg = new NetworkChatReceivedMessage(chatPacket);
                LD.EventSystem.EventFlow.Broadcast(msg);
                break;
        }

    }

}