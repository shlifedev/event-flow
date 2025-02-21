using System.Collections.Generic;

namespace LD.Framework.EventFlow
{
    public interface IEventObserver<TListener> where TListener : IEventListenerMarker
    {
        IReadOnlyList<TListener> GetListeners(); 
        void RegisterListener(TListener listener);
        void UnregisterListener(TListener listener);
    }
}