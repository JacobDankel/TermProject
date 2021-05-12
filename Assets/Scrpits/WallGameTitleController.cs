using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallGameTitleController : MonoBehaviour
{
    public string startScene;
    public string gameScene;
    public bool playedBefore;
    // Start is called before the first frame update

    public void startGame()
    {
        if (playedBefore)
        {
            SceneManager.LoadScene(gameScene);
        }
        else
        {
            SceneManager.LoadScene(startScene);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
