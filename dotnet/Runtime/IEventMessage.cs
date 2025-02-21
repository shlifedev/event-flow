namespace LD.Framework.EventFlow
{
    /// <summary>
    /// Interface for Event Message
    /// </summary>
    public interface IEventMessage 
    {
        
    }

    public interface IEventMessage<TSender>
    {
        TSender Sender { get; }
    }
    
    
}