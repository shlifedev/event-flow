using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif
namespace LD.EventSystem
{  
    /// <summary>
    /// Objects that send and register game events
    /// </summary>
    public sealed class EventPipeline<TMessage> : IEventPipeline<IEventListenerMarker, TMessage> 
        where TMessage : IEventMessage
    { 
        
        #region Fields  
        /// <summary>
        /// Listener Objs
        /// </summary>
        private List<IEventListenerMarker> Listeners { get; } = new List<IEventListenerMarker>(); 
        /// <summary>
        /// Listener Hashs
        /// </summary>
        private HashSet<IEventListenerMarker> RegisteredHashMap { get; } = new HashSet<IEventListenerMarker>();

        #endregion
        #region Functions

        public IReadOnlyList<IEventListenerMarker> GetListeners()
        {
            return Listeners;
        }

        /// <summary>
        /// Regist Listener
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterListener(IEventListenerMarker listener)
        { 
            if (RegisteredHashMap.Contains(listener) == false)
            {
                Listeners.Add(listener);
                RegisteredHashMap.Add(listener);
            } 
        }

        
        /// <summary>
        /// Is Already Registred?
        /// </summary>
        /// <param name="listener"></param>
        /// <returns></returns>
        public bool IsRegistered(IEventListenerMarker listener)
        {
            return RegisteredHashMap.Contains(listener);
        }

        
        public void ClearListener()
        {
            Listeners.Clear();
            RegisteredHashMap.Clear();
        }
        
        /// <summary>
        /// Unregister
        /// </summary>
        /// <param name="listener"></param>
        public void UnregisterListener(IEventListenerMarker listener)
        { 
            if (!RegisteredHashMap.Contains(listener)) return;
            // Listeners.Add(listener);
            RegisteredHashMap.Remove(listener);
            Listeners.Remove(listener);
        }

        /// <summary>
        /// Emit Message to all listeners
        /// </summary> 
        public void EmitAll<TEventArgs>(TEventArgs args) where TEventArgs :  TMessage
        {
            if (Listeners.Count == 0)
                return; 
            
            for (int i=Listeners.Count-1; i>=0; --i)
            { 
                var listener = Listeners[i];
                if (listener is not IEventListener<TEventArgs> convert)
                {
#if UNITY_EDITOR
                    Debug.LogError($"{nameof(IEventListenerMarker)}  must be explicitly implemented with a generic argument.");
#else
                    Console.WriteLine($"{nameof(IEventListenerMarker)}  must be explicitly implemented with a generic argument.");
#endif
                }
                else 
                {  
                    convert.OnEvent(args);
                }
            }  
            
        } 

        

        public void BroadcastTo<TEventArgs>(TEventArgs args, IEventListener<TEventArgs> target) where TEventArgs :  TMessage
        { 
                 target.OnEvent(args);
        }    
        #endregion
    }
    
    
    
    
    
}