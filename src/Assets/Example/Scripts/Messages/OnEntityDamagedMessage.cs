using System.Collections;
using System.Collections.Generic;
using LD.Framework;
using LD.Framework.EventFlow;
using Test;
using UnityEngine;

public struct OnEntityDamagedMessage : IEventMessage
{
     public GameEntity Target;
     public float PreviousHealth;
     public float CurrentHealth; 
}
