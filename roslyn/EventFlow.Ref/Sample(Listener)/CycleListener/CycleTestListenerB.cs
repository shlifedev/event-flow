using LD.EventSystem;

namespace LD.EventSystem.Ref.Listeners.CycleTest;


public class CycleTestListenerB : IEventListener<CycleMessage>
{
    public void OnEvent(CycleMessage args)
    {
        throw new NotImplementedException();
    }
}