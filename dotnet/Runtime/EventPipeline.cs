using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif
namespace LD.Framework.EventFlow
{
    /*
     * Todo : 구조상 TMessage에 대해서 구독하고있는 리스너 정보들을 저장함
     * 1:n 관계로 메세지에 대한 리스너가 여러개일 경우에 대해서도 처리 가능
     */
    
    /*
     * Todo 개선 가능사항
     * 현재는 TMessage(1)에 대해 여러 리스너(IEventListenerMarker)들이 등록되어있음
     * 이를 IEventListener<TMessage>로 변경하여 리스너가 TMessage에 대해 구독하고있음을 명시적으로 알 수 있도록 변경하고
     * EmitAll 의 루프를 최소화 할 수있음
     *
     * 혹은 JobSystem을 사용하여 멀티스레드로 처리할 수 있음
     */
    
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