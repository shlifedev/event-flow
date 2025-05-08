using System.Collections;
using UnityEngine;

namespace Example
{
    public class ExampleBaseMono : MonoBehaviour
    {
        public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // 최소 크기
        public Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f); // 최대 크기
        public float speed = 1.0f; // 크기 변화 속도
        public float duration = 1f; // PingPong 지속 시간 (0이면 무한)
    
  
        protected IEnumerator PingPongScale(float duration)
        {
            float elapsedTime = 0f; // 경과 시간
            float pingPongTime = 0f; // Mathf.PingPong 함수에 사용될 시간 변수

            while (duration <= 0f || elapsedTime < duration) // duration이 0이하면 무한, 0보다 크면 해당 시간 동안 실행
            {
                // Mathf.PingPong을 사용하여 0과 1 사이의 값을 왕복하도록 만듭니다.
                pingPongTime = Mathf.PingPong(Time.time * speed, 1.0f);

                // Lerp 함수를 사용하여 minScale과 maxScale 사이를 부드럽게 보간합니다.
                transform.localScale = Vector3.Lerp(minScale, maxScale, pingPongTime);
            
                elapsedTime += Time.deltaTime; // 경과 시간 업데이트
            
                yield return null; // 다음 프레임까지 대기
            }

            // (선택 사항) duration이 설정된 경우, 코루틴이 끝날 때 오브젝트를 특정 상태로 설정.  예: 원래 크기로 복귀
            transform.localScale = Vector3.one; // 원래 스케일 (1,1,1) 로
            // 또는
            // transform.localScale = minScale;  // 최소 스케일로
            // 또는
            // Destroy(gameObject);   // 오브젝트 파괴

        }

        // (선택 사항) 외부에서 코루틴을 중지시킬 수 있는 함수
        protected void StopPingPong()
        {
            StopAllCoroutines();
            //(선택) 중지 시, 원하는 크기로 설정.  예를 들어,
            transform.localScale = Vector3.one;
        }
    }
}