---
sidebar_position: 0
---
 
# OpenUPM

다음은 `openupm`을 사용하여 프로젝트에 메시지 시스템을 적용하는 예시입니다.


## OpenUPM이 설치되어 있는 경우

프로젝트의 루트 경로에서 다음을 실행합니다.

```
openupm add com.shlifedev.eventflow
```


## OpenUPM이 설치되어 있지 않은 경우

- [node.js](https://nodejs.org/en/download/)를 먼저 설치해야 합니다.

### Window의 경우 (node.js 설치)
```
# Download and install fnm:
winget install Schniz.fnm
# Download and install Node.js:
fnm install 22
# Verify the Node.js version:
node -v # Should print "v22.14.0".
# Verify npm version:
npm -v # Should print "10.9.2".
```

### OSX의 경우 (node.js 설치)
```
# Download and install fnm:
curl -o- https://fnm.vercel.app/install | bash
# Download and install Node.js:
fnm install 22
# Verify the Node.js version:
node -v # Should print "v22.14.0".
# Verify npm version:
npm -v # Should print "10.9.2".
```


### 그 외
[node.js](https://nodejs.org/en/download/) 공식 사이트를 확인 해 주세요.
openupm-cli 을 먼저 설치해야 합니다.  터미널(명령프롬프트) 를 



이후 다음 을 호출하여 openupm-cli 설치를 완료합니다.

```
npm install -g openupm-cli
```

이후 [OpenUPM이 설치되어 있는 경우](#openupm이-설치되어-있는-경우) 항목으로 돌아가 설치를 완료하세요.