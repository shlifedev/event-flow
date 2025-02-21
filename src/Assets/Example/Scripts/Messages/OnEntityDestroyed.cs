using System.Collections;
using System.Collections.Generic;
using LD.Framework;
using LD.Framework.EventFlow;
using Test;
using UnityEngine;

public struct OnEntityDestroyed : IEventMessage
{
     public GameEntity Target; 
}
