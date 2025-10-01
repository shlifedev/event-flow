using LD.EventSystem;
using LD.EventSystem.Attributes;
using UnityEngine;

namespace Example.Tutorial01_BasicMessage
{ 
    public partial struct CubeClickMessage : IEventMessage
    {
        public CubeClickMessage(GameObject obj)
        {
            this.Cube = obj;
        }
        public GameObject Cube { get; }
    }
}