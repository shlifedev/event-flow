using Example.Tutorial01_BasicMessage;
using LD.EventSystem;
using UnityEngine;

public class JustCube : MonoBehaviour
{
    float delay = 0;
    void OnMouseDown()
    {
        if (delay > 0) return;

        CubeClickMessage msg = new CubeClickMessage(this.gameObject);
        EventFlow.Broadcast<CubeClickMessage>(msg);
        delay = 1;
    }

    void Update()
    {
        delay -= Time.deltaTime;
    }
}
