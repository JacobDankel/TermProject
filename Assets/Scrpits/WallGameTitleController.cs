using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallGameTitleController : MonoBehaviour
{
    public string startScene;
    // Start is called before the first frame update

    public void startGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
