using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Star : MonoBehaviour, IMessageReceiver, IUnitTransferable
{
    [SerializeField] private int units = 0;

    private int Units
    {
        get { return units; }
        set { units = value; UpdateText(); }
    }


    void Start()
    {
        UpdateText();
        MessageManager.StartReceivingMessage<UnitTransferMessage>(this);
    }

    void Update()
    {
        Units++;
    }

    void UpdateText()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = Units.ToString();
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
