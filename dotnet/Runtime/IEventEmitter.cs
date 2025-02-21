using Cysharp.Threading.Tasks;

namespace LD.Framework.EventFlow
{
    public interface IEventEmitter<TMessage> where TMessage : IEventMessage
    {
        /// <summary>
        /// Send Event To Listener
        /// </summary> 
        UniTask EmitAll<TEventArgs>(TEventArgs args) where TEventArgs : TMessage;
    }
}