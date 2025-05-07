---
sidebar_position: -1
---
 
# .unitypackage 로 설치하기

`EventFlow`는 `.unitypackage`로 설치하는것이 가장 올바른 방법입니다. `openupm`을 사용하는 사용자에게는 안 좋은 소식이지만 
`openupm`을 지원하지 않는 이유는 유니티가 **packages로 관리되는 종속성에 대해서 DLC를 지원하지 않기 때문입니다.**

현재는 cysharp 같은 개발자의 리포지토리에서 찾아볼 수 있는 `zstring` `messagepack` 같은 패키지들도 외부 종속성 문제로
upm 설치와 별도로 nuget을 통해 필요한 dll을 설치해야하는 상황입니다.

따라서 현재는 `.unitypackage`로 설치하는것이 가장 올바른 방법입니다!

## 설치 방법

[github release](https://github.com/shlifedev/event-flow/releases/)페이지에서 최신 버전의 `.unitypackage`를 다운로드합니다