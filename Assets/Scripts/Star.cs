﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Star : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private int units = 0;
    [SerializeField] private Owner owner = Owner.neutral;
    [SerializeField] private GameObject selectedOutline = null;
    [SerializeField] private Star[] neighbors = null;

    Dictionary<Owner, Color> colormapping = new Dictionary<Owner, Color>()
{
    { Owner.player0, Color.red },
    { Owner.player1, Color.yellow },
    { Owner.player2, Color.blue },
    { Owner.player3, Color.green },
    { Owner.neutral, Color.grey }
};

    public enum Owner
    {
        player0,
        player1,
        player2,
        player3,
        neutral,
    }

    private void SetOwner(Owner owner)
    {
        this.owner = owner;
        SetColour();
    }

    public int Units
    {
        get { return units; }
        private set { units = value; UpdateText(); }
    }

    private void SetColour()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = colormapping[owner];
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
        UpdateText();
        MessageManager.StartReceivingMessage<UnitTransferMessage>(this);
        MessageManager.StartReceivingMessage<StarSelectedMessage>(this);
        MessageManager.StartReceivingMessage<AllStarsUnselectedMessage>(this);
        MessageManager.StartReceivingMessage<TickMessage>(this);
        SetColour();
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
        message.owner = owner;
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
            if (unitTransferMessage.sender == this)
            {
                DecreaseUnits(unitTransferMessage.amount);
            }
            if (unitTransferMessage.receiver == this)
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
            if (owner != Owner.neutral)
                IncreaseUnits(1);
        }
    }

    void OnDrawGizmos()
    {
        foreach (var neighbor in neighbors)
        {
            if (neighbor != null)
            {
                var from = transform.position;
                var to = neighbor.transform.position;
                var dir = (to - from).normalized;
                var right = Vector3.Cross(dir, new Vector3(0, 0, 1));
                var offset = right.normalized * 0.1f;

                from += offset;
                to += offset;

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(from, to);
                Gizmos.DrawLine(to, to + offset - dir);
            }
        }
    }
}
