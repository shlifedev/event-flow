using System;
using LD.EventSystem;
using LD.EventSystem.Attributes;
using TMPro;
using UnityEngine;

namespace Example4
{


    [EventFlowListener]
    public partial class ChatBox : MonoBehaviour,
        IEventListener<NetworkChatReceivedMessage>
    {
        private TextMeshProUGUI _chat;

        private void Awake()
        {
            this._chat ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            EventFlow.Register(this);
        }

        private void OnDisable()
        {
            EventFlow.UnRegister(this);
        }

        public void OnEvent(NetworkChatReceivedMessage args)
        {
            _chat.text += args.Sender + ":" + args.Message + "\n";
        }
    }
}