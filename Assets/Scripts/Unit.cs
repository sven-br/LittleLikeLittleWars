using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour, IMessageReceiver
{
    public Vector2 Direction { get; set; }
    public ObjectOwner Owner { get; set; }
    public Star Sender { get; set; }
    public int Amount { get; set; }
    private bool collisionDetected = false;

    void Start()
    {
        SetColour();
        MessageManager.StartReceivingMessage<SpaceFightMessage>(this);
    }

    void Update()
    {
        var speed = 0.5f;
        var velocity = Direction * Time.deltaTime * speed;
        transform.Translate(velocity);
    }
    private void FixedUpdate()
    {
        collisionDetected = false;
    }
    void UpdateText()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = Amount.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            var star = contact.collider.gameObject.GetComponent<Star>();

            if (star != null && star != Sender && !collisionDetected)
            {
                collisionDetected = true;
                var message = MessageProvider.GetMessage<UnitReceiveMessage>();
                message.amount = Amount;
                message.owner = Owner;
                message.receiver = star;
                MessageManager.SendMessage(message);
                Destroy(gameObject);
            }

            var unit = contact.collider.gameObject.GetComponent<Unit>();

            if (unit != null && unit.Owner != Owner && !(collisionDetected || unit.collisionDetected))
            {
                collisionDetected = true;
                var message = MessageProvider.GetMessage<SpaceFightMessage>();
                message.opponent1 = this;
                message.amount1 = Amount;
                message.opponent2 = unit;
                message.amount2 = unit.Amount;
                MessageManager.SendMessage(message);

            }
        }
    }

    private void SetColour()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = ColourMapping.colormapping[Owner];
    }

    private void ReduceAmount(int amount)
    {
        Amount = Mathf.Max(Amount-amount,0);
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is SpaceFightMessage)
        {
            var spaceFightMessage = message as SpaceFightMessage;
            if (spaceFightMessage.opponent2 == this)
            {
                collisionDetected = true;
                ReduceAmount(spaceFightMessage.amount1);
                if (Amount < 1) Destroy(gameObject);
                UpdateText();
            }
            else if (spaceFightMessage.opponent1 == this)
            {
                collisionDetected = true;
                ReduceAmount(spaceFightMessage.amount2);
                if (Amount < 1) Destroy(gameObject);
                UpdateText();
            }
        }
    }
}
