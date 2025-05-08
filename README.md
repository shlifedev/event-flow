# EventFlow

이벤트를 송/수신 하는데 전혀 GC(Memory)에 부담없게 설계 된 이벤트 시스템입니다.

- 매우 쉽게 각 객체간 디커플링을 달성할 수 있습니다.  

- 각 객체의 역할이 확실하기에, 깔끔한 이벤트 시스템을 게임에 적용할 수 있습니다.

- 사용하기 매우 쉽습니다.

- 이벤트 리스너 상속과, 등록만 확실히 했다면 휴먼 오류 없는 코드 작성이 가능합니다. (추후 Roslyn으로 코드검사까지 할 예정)
 
## [Example](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example)
[Movie_001.webm](https://github.com/user-attachments/assets/19ef0dd3-7288-49fa-b3c3-87b2195be071)

 

## How to use

### [Declare Your Game Event](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/Messages/OnEntityDamagedMessage.cs)
 IEventMessage를 상속받은 **구조체를** 선언하세요. 그 안에는 원하는 내용을 작성하세요.

```
 public struct YourMessage : IEventMessage{ 
   public string Message;
 }
```

### [Inherit IEventListenr<T> And Regist](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/HealthBarUI.cs)

IEventListener<TMessage> 를 상속하세요. 

이후 OnEnable, OnDisable에서 Register, Unregister 메서드를 1회씩 호출해줍니다. (메세지 등록을 위해)


```cs


public class YourClass : MonoBehaviour, IEventListener<YourMessage>{
    void OnEnable(){
         EventFlow.Register(this);
    }
    void OnDisable(){
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

