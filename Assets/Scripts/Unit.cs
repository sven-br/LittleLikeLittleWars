using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IMessageReceiver
{
    public Vector2 Direction { get; set; }
    public ObjectOwner Owner { get; set; }
    public Star Sender { get; set; }
    public int Amount { get; set; }
    private bool collisionDetected = false; // Stars brauchen das auch, für den Fall dass zwei Units gleichzeitig mit dem star colliden?

    void Start()
    {
        SetColour();
        MessageManager.StartReceivingMessage<SpaceFightMessage>(this);
    }

    void Update()
    {
        var speed = 1.0f;
        var velocity = Direction * Time.deltaTime * speed;
        transform.Translate(velocity);
    }
    private void FixedUpdate()
    {
        collisionDetected = false;
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

            if (unit != null && !collisionDetected) // hier schon checken ob der collision-Partner Freund oder Feind ist, statt owner mit in die SpaceFightMessage zu geben?
            {
                collisionDetected = true;
                var message = MessageProvider.GetMessage<SpaceFightMessage>();
                message.amount = Amount;
                message.owner = Owner;
                message.opponent = unit;
                MessageManager.SendMessage(message);
                Debug.Log(Owner + ": Spacefightmessage sent. I have units: " + Amount);
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
        if (Amount > amount) Amount -= amount; else Amount = 0;
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is SpaceFightMessage)
        {
            var spaceFightMessage = message as SpaceFightMessage;
            if (spaceFightMessage.owner != Owner && spaceFightMessage.opponent == this)
            {
                Debug.Log(Owner + ": Amount is " + Amount);
                Debug.Log(Owner + " Reducing amount by " + spaceFightMessage.amount);
                ReduceAmount(spaceFightMessage.amount);
                Debug.Log(Owner + " Amount is now " + Amount);
                if (Amount < 1) Destroy(gameObject);
            }
        }
    }
}
