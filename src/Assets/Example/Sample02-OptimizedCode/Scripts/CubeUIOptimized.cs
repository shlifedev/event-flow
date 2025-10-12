using System.Collections;
using Example.Tutorial01_BasicMessage;
using LD.EventSystem;
using LD.EventSystem.Attributes;
using TMPro;
using UnityEngine;


namespace Example2
{
    // ** Just Add this and make class to partial class. => 
    // So, Auto Generated Event Register function By Roslyn.
    [EventFlowListener]
    public partial class CubeUIOptimized : MonoBehaviour,
    IEventListener<CubeClickMessage>
    {
        [SerializeField] TextMeshProUGUI _text;

        [SerializeField] GameObject _owner;
        Coroutine _managedCoroutine;

        void OnEnable()
        { 
            this.RegisterEventListener();
//EventFlowOptimized.Register(this); // Event Register
        }

        void OnDisable()
        {
            this.UnregisterEventListener();
            //EventFlowOptimized.UnRegister(this); // Unregister
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
}