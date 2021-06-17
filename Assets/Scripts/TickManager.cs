using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] float intervalSeconds = 1.0f;

    float elapsedTime = 0;

    void Start()
    {
        elapsedTime = Time.time;
    }

    void Update()
    {
        

        if (elapsedTime + intervalSeconds < Time.time)
        {
            elapsedTime += intervalSeconds;

            var tickMessage = MessageProvider.GetMessage<TickMessage>();
            MessageManager.SendMessage(tickMessage);
        }
    }
}
