using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LD.Framework.EventFlow.Tests
{
    public class EventSubscriber : MonoBehaviour, IEventListener<PrimitiveSturctEventMessage>
    {
        public void Awake() => EventFlow.Register(this);

        private int _v;
        public void OnDestroy()
        {
            EventFlow.Unregister(this);
            Counter -= _v;
        }

        public static int Counter = 0;

        public void OnEvent(PrimitiveSturctEventMessage args)
        {
            Counter += args.Value; 
            this._v = args.Value;
        }
    }
}