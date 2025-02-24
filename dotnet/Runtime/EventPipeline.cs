using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LD.Framework.EventFlow
{
    /// <summary>
    /// Objects that send and register game events 
    /// </summary>
    public class EventPipeline<TMessage> : IEventPipeline<IEventListenerMarker, TMessage> 
        where TMessage : IEventMessage
    {
        #region Fields  
        /// <summary>
        /// Listener Objs
        /// </summary>
        protected virtual List<IEventListenerMarker> Listeners { get; } = new List<IEventListenerMarker>(); 
        /// <summary>
        /// Listener Hashs
        /// </summary>
        protected virtual HashSet<IEventListenerMarker> RegisteredHashMap { get; } = new HashSet<IEventListenerMarker>();
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
        public virtual UniTask EmitAll<TEventArgs>(TEventArgs args) where TEventArgs :  TMessage
        {  
            for (int i=Listeners.Count-1; i>=0; --i)
            { 
                var listener = Listeners[i]; 
                var convert = listener as IEventListener<TEventArgs>;
                if (convert == null)
                {
                    Debug.LogError($"{nameof(IEventListenerMarker)}  must be explicitly implemented with a generic argument.");
                }
                else 
                {  
                    return convert.OnEvent(args);
                }
            }  
            return UniTask.CompletedTask;
        } 
         
        public virtual UniTask BroadcastTo<TEventArgs>(TEventArgs args, IEventListener<TEventArgs> target) where TEventArgs :  TMessage
        { 
                return target.OnEvent(args);
        }    
        #endregion
    }
    
    
    
    
    
}