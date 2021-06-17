using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IMessageReceiver
{
    private void Start()
    {
        MessageManager.StartReceivingMessage<StarOwnerChangedMessage>(this);
    }

    void CheckWinCondition(StarState[] stars)
    {
        Debug.Log("CheckWinCondition");
        var owner = stars[0].owner;
        var win = owner == ObjectOwner.player0;

        foreach (var star in stars)
        {
            if (star.owner != owner)
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
        if (message is StarOwnerChangedMessage)
        {
            var starOwnerChangedMessage = (StarOwnerChangedMessage) message;
            var stars = starOwnerChangedMessage.state.stars;
            CheckWinCondition(stars);
        }
    }
}
