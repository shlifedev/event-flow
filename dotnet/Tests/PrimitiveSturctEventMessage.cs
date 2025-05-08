using UnityEngine;

namespace  LD.Framework.EventFlow.Tests
{
    public struct PrimitiveSturctEventMessage : IEventMessage
    {
        public PrimitiveSturctEventMessage(GameObject sender, int value)
        {
            this.Value = value;
            this.Sender = sender;
        }

        public GameObject Sender { get; }
        public int Value { get; }
    }
}