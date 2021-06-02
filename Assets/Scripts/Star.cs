using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, IMessageReceiver, IUnitTransferable
{
    [SerializeField] private int units = 0;

    void Start()
    {
        MessageManager.StartReceivingMessage<UnitTransferMessage>(this);
    }

    void Update()
    {
        var message = MessageProvider.GetMessage<UnitTransferMessage>();
        message.sender = this;
        MessageManager.SendMessage(message);
    }

    void OnMouseDown()
    {
        var message = MessageProvider.GetMessage<StarClickedMessage>();
        message.star = this;
        MessageManager.SendMessage(message);
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        var unitTransferMessage = (UnitTransferMessage) message;
        if (unitTransferMessage != null)
        {
            if ((Object)unitTransferMessage.receiver == this)
            {
                Debug.Log("star received a unit");
            }
        }
    }
}
