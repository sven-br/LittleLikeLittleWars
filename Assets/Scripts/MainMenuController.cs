using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public static void LoadScene(string name)
    {
        MessageManager.Clear();
        SceneManager.LoadScene(name);
    }
}
