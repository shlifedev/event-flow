using LD.EventSystem;
using LD.EventSystem.Attributes;
using LD.EventSystem.Ref.Messages;

public struct __ : IEventMessage
{

}
[EventFlowListener]
public partial class NoNamespace : IEventListener<__>
{
    public void OnEvent(__ args)
    {
        throw new NotImplementedException();
    }
}