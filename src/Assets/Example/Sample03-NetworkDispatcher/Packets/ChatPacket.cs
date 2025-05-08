public struct ChatPacket : IPacket
{
    public int Id { get; }


    public int PacketType => 1000;

    public int sender;
    public string message;

}