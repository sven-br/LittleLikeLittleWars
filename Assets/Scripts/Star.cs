﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Star : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private int units = 0;
    [SerializeField] private ObjectOwner owner = ObjectOwner.neutral;
    [SerializeField] private GameObject selectedOutline = null;
    [SerializeField] private SpawnInterval interval = SpawnInterval.medium;
    private int tickcount = 0;
    private List<Star> neighbors;
    private bool registered = false;

    public enum SpawnInterval
    {
        fast = 2,
        medium,
        slow,
    }

    public ObjectOwner Owner
    {
        get { return owner; }
        private set
        {
            owner = value;
            SetColour();

            var message = MessageProvider.GetMessage<StarOwnerChangedMessage>();
            MessageManager.SendMessage(message);
        }
    }

    public int Units
    {
        get { return units; }
        private set { units = value; UpdateText(); }
    }

    private void SetColour()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = ColourMapping.colormapping[owner];
    }

    public bool HasNeighbor(Star other)
    {
        foreach (var neighbor in neighbors)
        {
            if (neighbor == other)
                return true;
        }
        return false;
    }
    void Start()
    {
        neighbors = new List<Star>();

        UpdateText();
        SetColour();

        MessageManager.StartReceivingMessage<RegisterLinkMessage>(this);
        MessageManager.StartReceivingMessage<UnitReceiveMessage>(this);
        MessageManager.StartReceivingMessage<UnitSendMessage>(this);
        MessageManager.StartReceivingMessage<StarSelectedMessage>(this);
        MessageManager.StartReceivingMessage<AllStarsUnselectedMessage>(this);
        MessageManager.StartReceivingMessage<TickMessage>(this);
    }

    void Update()
    {
        if (!registered)
        {
            registered = true;
            var message = MessageProvider.GetMessage<RegisterStarMessage>();
            message.star = this;
            MessageManager.SendMessage(message);
        }
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
        message.owner = owner;
        MessageManager.SendMessage(message);
    }

    private void IncreaseUnits(int amount)
    {
        Units += amount;
    }

    private void DecreaseUnits(int amount)
    {
        Units -= amount;
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is RegisterLinkMessage)
        {
            var registerLinkMessage = message as RegisterLinkMessage;
            var link = registerLinkMessage.link;
            var neighbor = link.GetOtherStar(this);

            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

        else if (message is UnitReceiveMessage)
        {
            var unitReceiveMessage = message as UnitReceiveMessage;
            
            if (unitReceiveMessage.receiver == this)
            {
                if (unitReceiveMessage.owner == this.owner)
                {
                    IncreaseUnits(unitReceiveMessage.amount);
                }
                else
                {
                    int diff = units - unitReceiveMessage.amount;

                    if (diff > 0)
                    {
                        DecreaseUnits(unitReceiveMessage.amount);
                    }
                    else if (diff < 0)
                    {
                        Owner = unitReceiveMessage.owner;
                        Units = (-diff);
                    }
                    else
                    {
                        DecreaseUnits(unitReceiveMessage.amount);
                        Owner = ObjectOwner.neutral;
                    }
                        
                }
                
            }
        }

        else if (message is UnitSendMessage)
        {
            var unitSendMessage = message as UnitSendMessage;
            if (unitSendMessage.sender == this) DecreaseUnits(unitSendMessage.amount);
        }

        else if (message is StarSelectedMessage)
        {
            var starSelectedMessage = message as StarSelectedMessage;
            if ((Object)starSelectedMessage.star == this)
            {
                UpdateSelected(true);
            }
        }

        else if (message is AllStarsUnselectedMessage)
        {
            var allStarsUnselectedMessage = message as AllStarsUnselectedMessage;
            UpdateSelected(false);
        }

        else if (message is TickMessage)
        {
            if (owner != ObjectOwner.neutral)
            {
                tickcount++;
                    if (tickcount == (int)interval)
                    {
                        IncreaseUnits(1);
                        tickcount = 0;
                    }
               
            }
        }
    }
}
