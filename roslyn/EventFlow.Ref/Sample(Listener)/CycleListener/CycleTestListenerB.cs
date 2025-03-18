using LD.Framework.EventFlow;

namespace EventFlow.Ref.Listeners.CycleTest;


public class CycleTestListenerB : IEventListener<CycleMessage>
{
    public void OnEvent(CycleMessage args)
    {
        throw new NotImplementedException();
    }
}