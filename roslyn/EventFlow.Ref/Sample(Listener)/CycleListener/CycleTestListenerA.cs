using LD.Framework.EventFlow;

namespace EventFlow.Ref.Listeners.CycleTest;

public class CycleTestListenerA : IEventListener<CycleMessage>
{
    public void OnEvent(CycleMessage args)
    {
         
    }
}