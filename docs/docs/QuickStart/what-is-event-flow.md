---
sidebar_position: 0
---

# EventFlow는 무엇인가요?

EventFlow는 유니티엔진에서 쉽게 이벤트기반 프로그래밍을 할 수 있도록 설계된 무료 라이브러리입니다. 

## 사용 예시
 
 -ㅅ-
 
### Declare Your Game Event
Just inherit the IEventMessage to the structure.

```
 public struct YourEvent : IEventMessage{ 
   public string Message;
 }
```

### Inherit IEventListenr

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