
using LD.EventSystem;
using LD.EventSystem.Attributes;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    [EventFlowListener]
    public partial class HealthBarUI : MonoBehaviour,
        IEventListener<OnEntityDamagedMessage>,
        IEventListener<OnEntityDestroyed>

    {


        public GameEntity Target; 
        public TextMeshProUGUI Text;
        public Image FillImage;

        private void OnEnable()
        {
            // Register this class to listen to the event
            LD.EventSystem.EventFlow.Register(this);
            FillImage.fillAmount = (float)Target.Health / Target.MaxHealth;
            Text.text = Target.Health.ToString("0.0");
             
        }


        private void OnDestroy()
        {
            LD.EventSystem.EventFlow.UnRegister(this);
        }

        private void OnDisable()
        {
            LD.EventSystem.EventFlow.UnRegister(this);
        }

        private void LateUpdate()
        {
            this.transform.position = Target.transform.position;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);
        }

        public void OnEvent(OnEntityDamagedMessage args)
        {
            Debug.Log($"[HealthBarUI] {args.Target.name} took damage => {args.PreviousHealth - args.CurrentHealth}");
            if (args.Target == this.Target)
            { 
                FillImage.fillAmount = (float)Target.Health / Target.MaxHealth;
                Text.text = args.CurrentHealth.ToString("0.0");
            } 
        }


    
        public void OnEvent(OnEntityDestroyed args)
        {
            if (args.Target == this.Target)
            {
                Debug.Log($"[HealthBarUI] {args.Target.name} has been destroyed");
                Destroy(this.gameObject);
            }
        }
    }
}