using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        var message = MessageProvider.GetMessage<NextSceneMessage>();
        MessageManager.SendMessage(message);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
