using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public ObjectOwner Owner { get; set; }
    public Star Sender { get; set; }
    public int Amount { get; set; }

    void Start()
    {
        SetColour();
    }

    void Update()
    {
        var speed = 1.0f;
        var velocity = Direction * Time.deltaTime * speed;
        transform.Translate(velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            var star = contact.collider.gameObject.GetComponent<Star>();

            if (star != null && star != Sender)
            {
                var message = MessageProvider.GetMessage<UnitReceiveMessage>();
                message.amount = Amount;
                message.owner = Owner;
                message.receiver = star;
                MessageManager.SendMessage(message);
                Destroy(gameObject);
            }

            var unit = contact.collider.gameObject.GetComponent<Unit>();

            if (unit != null)
            {

            }
        }
    }
    private void SetColour()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = ColourMapping.colormapping[Owner];
    }
}
