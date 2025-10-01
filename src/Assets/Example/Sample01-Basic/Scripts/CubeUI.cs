using System.Collections;
using Example.Tutorial01_BasicMessage;
using LD.EventSystem;
using TMPro;
using UnityEngine;

public class CubeUI : MonoBehaviour, IEventListener<CubeClickMessage>
{
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] GameObject _owner;
    Coroutine _managedCoroutine;

    void OnEnable()
    {
        LD.EventSystem.EventFlow.Register(this); // Event Register
    }

    void OnDisable()
    {  
        LD.EventSystem.EventFlow.UnRegister(this); // Unregister
    }

    public void OnEvent(CubeClickMessage args)
    {
        if (args.Cube == _owner)
        {
            _text.SetText(args.Cube.name + " click!");
            _managedCoroutine = StartCoroutine(TextReset());
        }
    }


    IEnumerator TextReset()
    {
        yield return new WaitForSeconds(1);
        _text.SetText("'___'");
        _managedCoroutine = null;
    }


    void OnDestroy()
    {
        if (_managedCoroutine != null)
        {
            StopCoroutine(_managedCoroutine);
            _managedCoroutine = null;
        }

        EventFlow.UnRegister(this);
    }
}