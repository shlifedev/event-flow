using System.Collections;
using System.Collections.Generic;
using LD.EventSystem;
using Test;
using UnityEngine;

public struct OnEntityDestroyed : IEventMessage
{
     public GameEntity Target; 
}
