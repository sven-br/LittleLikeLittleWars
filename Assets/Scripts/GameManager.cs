using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Star[] stars;

    void Update()
    {
        CheckWinCondition();
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
