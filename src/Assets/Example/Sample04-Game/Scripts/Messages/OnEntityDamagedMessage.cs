
 
using LD.EventSystem;
using Test;

public struct OnEntityDamagedMessage : IEventMessage
{
     public GameEntity Target;
     public float PreviousHealth;
     public float CurrentHealth; 
}
