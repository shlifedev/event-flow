using System.Collections;
using System.Collections.Generic;
using LD.Framework;
using LD.Framework.EventFlow;
using unity_event_system.GameEvent.Example.Scripts.Messages;
using UnityEngine;

public struct OnEntityDestroyed : IEventMessage
{
     public GameEntity Target; 
}
