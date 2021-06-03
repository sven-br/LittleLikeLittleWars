using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IMessageReceiver
{
    private List<Star> stars;

    private void Start()
    {
        stars = new List<Star>();
        MessageManager.StartReceivingMessage<RegisterStarMessage>(this);
    }

    void CheckWinCondition()
    {
        var owner = stars[0].getOwner();

        foreach (var star in stars)
        {
            if (star.getOwner() != owner)
            {
                return;
            }
        }

        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
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
