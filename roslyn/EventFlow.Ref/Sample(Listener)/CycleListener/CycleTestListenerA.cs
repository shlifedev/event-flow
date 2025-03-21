using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Listeners.CycleTest;

public class CycleTestListenerA : IEventListener<CycleMessage>
{
    public void OnEvent(CycleMessage args)
    {
         
    }
}