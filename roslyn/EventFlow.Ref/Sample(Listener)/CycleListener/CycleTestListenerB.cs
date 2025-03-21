using LD.Framework.EventFlow;

namespace LD.EventFlow.Ref.Listeners.CycleTest;


public class CycleTestListenerB : IEventListener<CycleMessage>
{
    public void OnEvent(CycleMessage args)
    {
        throw new NotImplementedException();
    }
}