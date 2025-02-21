namespace LD.Framework.EventFlow
{
    public interface IEventPipeline<TListener, TMessage> : IEventObserver<TListener>, IEventEmitter<TMessage>
        where TListener : IEventListenerMarker
        where TMessage : IEventMessage
    {
        
    }
}