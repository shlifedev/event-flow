namespace LD.Framework.EventFlow
{
    /// <summary>
    /// Classes to manage events for each message 
    /// </summary> 
    public static class EventFlowGeneric<TMessage> where TMessage : IEventMessage
    {
        private static EventPipeline<TMessage> Pipeline = new EventPipeline<TMessage>();  
        public static void EmitAll(TMessage message) => Pipeline.EmitAll(message); 
        public static void Register(IEventListenerMarker listener) => Pipeline.RegisterListener(listener); 
        public  static void Unregister(IEventListenerMarker listener) => Pipeline.UnregisterListener(listener); 
        public static void Clear() => Pipeline.ClearListener();
        
    }
}
