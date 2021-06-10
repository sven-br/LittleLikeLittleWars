using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour, IMessageReceiver
{
    private const int firstLevelNumber = 1;
    private const int lastLevelNumber = 2;

    private int currentLevel;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartReceivingMessages();
        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }

    void Update()
    {

    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        switch (message)
        {
            case NextSceneMessage msg:
                TransitionNextScene();
                break;
            case LevelFailedMessage msg:
                ReloadScene();
                break;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartReceivingMessages();
    }

    private void StartReceivingMessages()
    {
        MessageManager.StartReceivingMessage<NextSceneMessage>(this);
        MessageManager.StartReceivingMessage<LevelFailedMessage>(this);
    }

    public void TransitionNextScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "SplashScreen":
                LoadScene("MainMenu");
                break;
            case "MainMenu":
                LoadScene("Level" + firstLevelNumber);
                break;
            // is currentSceneName the name of a level?
            case var val when new Regex(@"^Level[0-9]+$").IsMatch(val):
                {
                    var currentLevelNumberString = new Regex(@"^Level([0-9]+)$").Split(val)[1];
                    var currentLevelNumber = int.Parse(currentLevelNumberString);

                    if (currentLevelNumber != lastLevelNumber)
                    {
                        LoadScene("Level" + (currentLevelNumber + 1));
                    }
                    else
                    {
                        LoadScene("MainMenu");
                    }

                    break;
                }
        }
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadScene(string name)
    {
        MessageManager.Clear();
        SceneManager.LoadScene(name);
    }
}
