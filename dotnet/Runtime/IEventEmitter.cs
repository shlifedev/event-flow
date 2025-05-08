namespace LD.Framework.EventFlow
{
    /// <summary>
    /// Interface for Event Emitter
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IEventEmitter<in TMessage> where TMessage : IEventMessage
    {
        /// <summary>
        /// Send Event To Listener
        /// </summary> 
        void EmitAll<TEventArgs>(TEventArgs args) where TEventArgs : TMessage;
    }
}