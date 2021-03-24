using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera playerCam;
    public Camera sceneCam;
    [Space]
    public float timeScale;

    private void Start()
    {
        playerCam.enabled = false;
        sceneCam.GetComponent<AudioListener>().enabled = true;
        sceneCam.enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = false;
        timeScale = Time.deltaTime;
    }

    public void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneCam.GetComponent<AudioListener>().enabled = !sceneCam.GetComponent<AudioListener>().enabled;
                sceneCam.enabled = !sceneCam.enabled;
                playerCam.GetComponent<AudioListener>().enabled = !playerCam.GetComponent<AudioListener>().enabled;
                playerCam.enabled = !playerCam.enabled;
            }
        }
    }

}
