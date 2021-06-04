using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour, IMessageReceiver
{

    private int tickcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        MessageManager.StartReceivingMessage<TickMessage>(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is TickMessage)
        {

        }
    }
}
