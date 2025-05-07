using System;
using UnityEngine;

    public class FakeChatSender : MonoBehaviour
    {
        private float remainTime = 1;
        public void Update()
        {
            if (remainTime < 0)
            {
                remainTime = UnityEngine.Random.Range(1, 3);
                NetworkManager.Instance.receivedPackets.Enqueue(new ChatPacket()
                {
                    sender = UnityEngine.Random.Range(1,10),
                    message = "Test Message"
                });
            }

            remainTime -= Time.deltaTime;
        }
    }