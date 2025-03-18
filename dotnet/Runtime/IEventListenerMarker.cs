namespace LD.Framework.EventFlow
{ 
    /// <summary>
    /// Marking interface to facilitate listener registration.
    /// </summary>
    public interface IEventListenerMarker
    { 
         
    } 
    
    /// <summary>
    /// A listener to listen for events
    /// In this case, convert the args directly and use them.
    /// The implementation of Dispose is mandatory to call when unsubscribing.
    /// </summary>
    public interface IEventListener<in TArgs> : IEventListenerMarker where TArgs : IEventMessage
    {
        void OnEvent(TArgs args);
    } 
}