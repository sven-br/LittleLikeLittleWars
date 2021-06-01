using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour, IMessageReceiver, IUnitTransferable
{
    public Star star0;
    public Star star1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        var unitTransferMessage = (UnitTransferMessage) message;
        if (unitTransferMessage != null)
        {
            if ((Object)unitTransferMessage.receiver == this)
            {
                Debug.Log("link received a unit");
            }
        }
    }
}
