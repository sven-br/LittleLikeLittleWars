using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IMessageReceiver
{
    private List<Star> stars;

    private void Start()
    {
        stars = new List<Star>();
        MessageManager.StartReceivingMessage<RegisterStarMessage>(this);
        MessageManager.StartReceivingMessage<StarOwnerChangedMessage>(this);
    }

    void CheckWinCondition()
    {
        Debug.Log("CheckWinCondition");
        var owner = stars[0].Owner;
        var win = owner == ObjectOwner.player0;

        foreach (var star in stars)
        {
            if (star.Owner != owner)
            {
                return;
            }
        }

        if (win)
        {
            var message = MessageProvider.GetMessage<NextSceneMessage>();
            MessageManager.SendMessage(message);
        }
        else
        {
            var message = MessageProvider.GetMessage<LevelFailedMessage>();
            MessageManager.SendMessage(message);
        }
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is RegisterStarMessage)
        {
            var registerStarMessage = message as RegisterStarMessage;
            stars.Add(registerStarMessage.star);
        }

        else if (message is StarOwnerChangedMessage)
        {
            CheckWinCondition();
        }
    }
}
