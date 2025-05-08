---
sidebar_position: 100000000
---

# 향후 계획

:::tip

업데이트 예정, 향후 EventFlow의 방향을 이 곳에서 확인하실 수 있습니다.

:::

## Roslyn 적용에 대한 고민

EventFlow를 사용해주셔서 감사합니다. 현재 저는 성능과 생산성, 그리고 프로그래머의 실수를 줄이기 위해 EventFlow에 `Roslyn` 적용을 고민하고 있습니다.

`Roslyn`을 적용하는 것은 어려운일이 아니지만 **소스제너레이터** 와 **구문분석기** 는 호스트(IDE) 성능에 영향을 주며 제가 이런 부분에서는 아직 기술적으로 부족하기 때문에 이런 개발환경의 성능 이슈를 100% 고려하면서 `Roslyn`을 도입하는것이 조금 두려웠습니다. 

현재 EventFlow의 문제들을 나열하고, `Roslyn`을 적용하면 어떤 이점을 얻을 수 있게 되는지 이 문서에 글을 작성해보려고 합니다. 


### 첫 번째 이유 : Reflection는 느립니다.

현재 EventFlow의 `Reigster` 및 `Unregister` 이벤트 구독 라이프사이클 관리를 위한 메서드는 처음 구독시 `Reflection` 에 의해 `GC` 가 발생하는 문제점이 있습니다. 이 문제는 성능에 큰 영향을 끼치지는 않지만 `Reflection` 기능은 다음과 같은 이유로 많은 C# 개발자들이 기피하는 이유중 하나입니다.

- 생산성 면에서는 편리하지만 자주 호출되거나, 한 번에 많은 호출이 필요할때** 메모리, CPU 사용량이 증가합니다.**
- 위와 같은 이유로 게임 프로젝트에서는 가급적 사용하고 싶지 않습니다.
- 코드의 변경을 추적하는데 한계가 있습니다. 예를 들어 `Register` 함수가 `Subscribe` 로 변경되는 경우 함수의 변경사항을 추적할 수 없습니다. (Attribute를 사용하면 해결할 수 있지만 단순한 사용에서는 코드가 지저분해집니다.)

예를 들어 `UserLoginMessage` 를 구독 한 스크립트가 있다고 가정하겠습니다.

```cs

public class UserComponent : MonoBehaviour, IEventListener<UserLoginMessage>{
    void OnRaised(UserLoginMessage message){
        // 
    }

    void Awake () => EventFlow.Register(this); # 등록
    void OnDestroy () => EventFlow.UnRegister(this); #해지
}

```

`EventFlow.Register(this)` 는 `Register` 함수 구현체에 현재 객체를 넘겨 타입을 확인 후 
상속받고있는 메세지들에 대해 핸들러에 **구독 요청**을 보냅니다. 

만약 UserComponent가 100, 1000개여도 현재 EventFlow는 단 한번만 리플렉션을 사용합니다. 
하지만 규모가 큰 프로젝트의 경우 이벤트의 종류 자체가 많아지면 결국 성능이슈로 이어지기 때문에 그런 상황 자체를 피하기 위해서 Roslyn을 도입해야합니다.

### 두 번째 이유 : 프로그래머의 실수 

`C++`와 같은 언어에서 * 포인터를 해제하지 않으면 어떻게 되죠? * 모두가 알다시피, 메모리 누수가 일어납니다. 또한 어플리케이션이 크래시되거나 예상하지 못한 문제가 일어나는 원인이 됩니다. 

EventFlow 의 경우도 마찬가지입니다. 구독을 적절하게 해지하지 않으면 예상할 수 없는 문제가 발생합니다. 

```cs

public class UserComponent : MonoBehaviour, IEventListener<UserLoginMessage>{
    void OnRaised(UserLoginMessage message){
        // 
    }

    void Awake () => EventFlow.Register(this); # 등록
    // void OnDestroy () => EventFlow.UnRegister(this); #해지
}

```

`OnDestroy` 함수를 주석처리한 채로 `UserComponent`가 부착된 오브젝트가 소멸하게 되면 여전히 EventFlow는 '구독' 상태이기 때문에
`UserComponent` 가 부착된 대상 오브젝트에 이벤트에 대해 통지를 요청합니다. 즉 엔진상에서 오브젝트는 삭제되었으나 EventFlow에 의해 UserComponent 가 아직도
메모리에 남아있을 확률이 커집니다.

Roslyn 도입을 하게되면 다음과 같은 이점을 얻습니다.

- 등록과 해지 과정 자체를 프로그래머가 통제하지 않아도 Roslyn을 사용하면 이 모든 과정을 자동화시킬 수 있습니다.
- 메모리누수, 오류 등에서 자유로워질 수 있습니다.




