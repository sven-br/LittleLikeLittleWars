﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Star : MonoBehaviour, IMessageReceiver, IUnitTransferable
{
    [SerializeField] private int units = 0;
    [SerializeField] private GameObject selectedOutline = null;

    public int Units
    {
        get { return units; }
        private set { units = value; UpdateText(); }
    }

    void Start()
    {
        UpdateText();
        MessageManager.StartReceivingMessage<UnitTransferMessage>(this);
        MessageManager.StartReceivingMessage<StarSelectedMessage>(this);
        MessageManager.StartReceivingMessage<AllStarsUnselectedMessage>(this);
        MessageManager.StartReceivingMessage<TickMessage>(this);
    }

    void Update()
    {
    }

    void UpdateText()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = Units.ToString();
    }

    void UpdateSelected(bool selected)
    {
        selectedOutline.SetActive(selected);
    }

    void OnMouseDown()
    {
        var message = MessageProvider.GetMessage<StarClickedMessage>();
        message.star = this;
        MessageManager.SendMessage(message);
    }

    private void IncreaseUnits(int amount)
    {
        Units += amount;
        Debug.Log("star received " + amount + " units");
    }

    private void DecreaseUnits(int amount)
    {
        Units -= amount;
        Debug.Log("star lost " + amount + " units");
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message.GetType() == typeof(UnitTransferMessage))
        {
            var unitTransferMessage = (UnitTransferMessage)message;
            if ((Object)unitTransferMessage.sender == this)
            {
                DecreaseUnits(unitTransferMessage.amount);
            }
            if ((Object)unitTransferMessage.receiver == this)
            {
                IncreaseUnits(unitTransferMessage.amount);
            }
        }

        if (message.GetType() == typeof(StarSelectedMessage))
        {
            var starSelectedMessage = (StarSelectedMessage)message;
            if ((Object)starSelectedMessage.star == this)
            {
                UpdateSelected(true);
            }
        }

        if (message.GetType() == typeof(AllStarsUnselectedMessage))
        {
            var allStarsUnselectedMessage = (AllStarsUnselectedMessage)message;
            UpdateSelected(false);
        }

        if (message.GetType() == typeof(TickMessage))
        {
            IncreaseUnits(1);
        }
    }
}
