using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera playerCam;
    public Camera sceneCam;
    public Cursor cursor;
    [Space]
    public int wallSize;
    public Transform LeftWall;
    private bool leftDone = false;
    public Transform TopWall;
    private bool topDone = false;
    public Transform RightWall;
    private bool rightDone = false;
    public Transform BottomWall;
    private bool bottomDone = true;
    [Space]
    public GameObject endBox;
    public int roundNum = 1;

    private IEnumerator startRound;
    private IEnumerator endRound;

    private void Start()
    {
        playerCam.enabled = false;
        sceneCam.GetComponent<AudioListener>().enabled = true;
        sceneCam.enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = false;

        startRound = ShrinkWalls(0.015f);
        endRound = FinishRound(0.5f);
        StartCoroutine(startRound);
    }

    public void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                sceneCam.GetComponent<AudioListener>().enabled = !sceneCam.GetComponent<AudioListener>().enabled;
                sceneCam.enabled = !sceneCam.enabled;
                playerCam.GetComponent<AudioListener>().enabled = !playerCam.GetComponent<AudioListener>().enabled;
                playerCam.enabled = !playerCam.enabled;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale *= 2;
            }
        }
        //print(wallScale/Time.time);
    }

    private IEnumerator ShrinkWalls(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (LeftWall.position.x <= endBox.transform.position.x - 15)
            {
                LeftWall.position = LeftWall.position + Vector3.right * 0.005f;
            }
            else leftDone = true;


            if (TopWall.position.y >= endBox.transform.position.y + 11)
            {
                TopWall.position = TopWall.position + Vector3.down * 0.003f;
            }
            else topDone = true;


            if (RightWall.position.x >= endBox.transform.position.x + 15)
            {
                RightWall.position = RightWall.position + Vector3.left * 0.005f;
            }
            else rightDone = true;


            if (BottomWall.position.y <= endBox.transform.position.y -11)
            {
                BottomWall.position = BottomWall.position + Vector3.up * 0.003f;
            }
            else bottomDone = true;


            if (leftDone && rightDone && bottomDone && topDone) StartCoroutine(endRound);
        }
    }
    private IEnumerator FinishRound(float waitTime)
    {
        yield return 0;
    }
}
