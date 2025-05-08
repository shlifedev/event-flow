using LD.EventSystem;
using UnityEngine;

namespace Example.Tutorial01_BasicMessage
{
    public struct CubeClickMessage : IEventMessage
    {
        public CubeClickMessage(GameObject obj)
        {
            this.Cube = obj;
        }
        public GameObject Cube { get; }
    }
}