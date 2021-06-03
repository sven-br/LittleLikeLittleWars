using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour, IMessageReceiver
{
    private List<Star> stars;
    private List<Link> links;

    void Start()
    {
        stars = new List<Star>();
        links = new List<Link>();

        MessageManager.StartReceivingMessage<RegisterStarMessage>(this);
        MessageManager.StartReceivingMessage<RegisterLinkMessage>(this);
    }

    void Update()
    {
        Debug.Log("stars: " + stars.Count);
        Debug.Log("links: " + links.Count);
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is RegisterStarMessage)
        {
            var registerStarMessage = message as RegisterStarMessage;
            stars.Add(registerStarMessage.star);
        }

        else if (message is RegisterLinkMessage)
        {
            var registerLinkMessage = message as RegisterLinkMessage;
            links.Add(registerLinkMessage.link);
        }
    }
}
