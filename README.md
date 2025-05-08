# EventFlow

A very easy to use zero-gc game event sending/listen system. 

- This is useful for decoupling UI code from game code.

- You can listen to any event that happens in your game where it inherits from IEventListener<>.

- Sending/receiving events does not incur any unnecessary GC. (struct only)

 
## [Example](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example)
[Movie_001.webm](https://github.com/user-attachments/assets/19ef0dd3-7288-49fa-b3c3-87b2195be071)

 

## How to use

### [Declare Your Game Event](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/Messages/OnEntityDamagedMessage.cs)
Just inherit the IEventMessage to the structure.

```
 public struct YourEvent : IEventMessage{ 
   public string Message;
 }
```

### [Inherit IEventListenr<T> And Regist](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/HealthBarUI.cs)
```cs
public class YourClass : MonoBehaviour, IEventListener<YourMessage>{
    void OnEnable(){
         EventFlow.Register(this);
    }
    void OnDestroy(){
         EventFlow.UnRegister(this);
    }

    public UniTask OnEvent(YourMessage args){
         Debug.Log("Received! => " + args.Message);
    }
}
```


### [Broadcast message](https://github.com/shlifedev/unity-event-system/blob/main/GameEvent/Example/Scripts/GameEntity.cs)
```cs
     EventFlow.Broadcast(new YourMessage(){Message="hi"});
```  


## Roslyn Source Generator

(I will translate this part later)

 EventFlow에 Roslyn Source Generator 기능이 추가되었습니다. 기존 EventFlow는 Reflection을 사용하여 메세지 리스너를 등록하고 해지했습니다.

 기존 방식을 그대로 사용할 수 있지만 완벽하게 ZeroGC로 동작할 수 있도록 리스너를 최적화 할 수 있습니다.

### EventFlowListener Attribute 을 사용한 최적화

 기존 리스너에 EventFlowListenerAttribute를 추가하면 Source Generator가 이를 인식하여 최적화된 리스너를 생성합니다. 
 
 다음 소스코드는 기존의 IEventListener 방식입니다. 


 ```cs
 public class ChatApplication : IEventListener<TestUserChatMessage>
{
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    } 
}
 ```

 그러나 이 리스너를 Register, Unregister 할 때 최초 리스너 타입에 대해 1회 메모리 할당이 일어납니다.

 이를 피하기 위해서 EventFlowListener Attribute를 추가합니다.

```cs
[EventFlowListener] /* partial modifer를 반드시 추가해야함. */
public partial class ChatApplication : IEventListener<TestUserChatMessage>
{
    public void OnEvent(TestUserChatMessage args)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt zz")}] chat received \t " + args.ChatMessage);
    } 
}
```

 EventFlowListener Attribute를 사용하려면 사용하려고 하는 class에 대해 partial modifer를 추가해야합니다. 

 이는 추후 EventFlowListener Attribute가 사용되는 리스너에 대해 Roslyn 레벨에서 최적화를 진행하기 위함입니다. 

