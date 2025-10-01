# EventFlow

EventFlow is a Pub-Sub event system that can be easily used universally in Unity or general .NET projects.


- Zero GC with Roslyn Source Generator
- Very Simple
- Intuitive


The following is an example of sending a message to deal damage when a game character is touched.
 
## [Example](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example)
[Movie_001.webm](https://github.com/user-attachments/assets/19ef0dd3-7288-49fa-b3c3-87b2195be071)

 

## How to use

### [Declare Your Game Event](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/Messages/OnEntityDamagedMessage.cs) 

```
 public struct YourMessage : IEventMessage{ 
   public string Message;
 }
```


## Very Simple Usage

### [Inherit IEventListenr<T> And Regist](https://github.com/shlifedev/event-flow/tree/main/src/Assets/Example/Scripts/HealthBarUI.cs)

Inherit & Subscribe IEventListener<TMessage> Your Class. 


```cs


[EventFlowListener]
public partial class YourClass : MonoBehaviour, IEventListener<YourMessage>{
    void OnEnable(){
         RegisterEventListener(this);
    }
    void OnDisable(){
         UnregisterEventListener(this);
    }

    public UniTask OnEvent(YourMessage args){
         Debug.Log("Received! => " + args.Message);
    }
}
```


### [Broadcast message](https://github.com/shlifedev/unity-event-system/blob/main/GameEvent/Example/Scripts/GameEntity.cs)

And Broadcast Your Message.

```cs
     EventFlow.Broadcast(new YourMessage(){Message="hi"});
```  



## FAQ

### Why should you use the EventFlowListener Attribute?

Internally, it uses the Roslyn source generator to help you subscribe to and unsubscribe from multiple message types without GC. If you use EventFlow.Register(this); instead, you don't have to use it, but we recommend using it.


