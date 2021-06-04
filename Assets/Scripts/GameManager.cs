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
        MessageManager.StartReceivingMessage<StarOwnerChangedMessage>(this);
    }

    void CheckWinCondition()
    {
        Debug.Log("CheckWinCondition");
        var owner = stars[0].Owner;

        foreach (var star in stars)
        {
            if (star.Owner != owner)
            {
                return;
            }
        }

        LoadScene("MainMenu");
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

    void LoadScene(string name)
    {
        MessageManager.Clear();
        SceneManager.LoadScene(name);
    }
}
