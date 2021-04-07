using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController Player;
    public Camera playerCam;
    public Camera sceneCam;
    public Color32 backgroundColor;
    public Cursor cursor;
    public AudioClip wallFinish;
    [Space]
    public Text healthText;
    public Image healthImage;
    public float CurrentHealth;
    [Space]
    public float timeLimit;
    public float timeRemaining;
    public Text timer;

    public float verticleMoveScalar;
    public float horizontalMoveScalar;
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
    public float endBoxVertBound;
    public float endBoxHorzBound;
    public int roundNum = 1;


    private void Start()
    {
        CurrentHealth = Player.hp;
        timeRemaining = timeLimit;

        //Calcuates the amount to move the walls
        verticleMoveScalar = 0.25f;
        horizontalMoveScalar = verticleMoveScalar * 2;

        //Randomizes the box position
        endBoxHorzBound = Random.Range(-14f, 14f);
        endBoxVertBound = Random.Range(-8f, 8f);
        endBox.transform.position = new Vector3(endBoxHorzBound, endBoxVertBound, 0);


        playerCam.enabled = false;
        sceneCam.GetComponent<AudioListener>().enabled = true;
        sceneCam.enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = false;

    }

    public void Update()
    {
        //Timer

        if (timeRemaining > 0)
        {
            //Reduces the time and also updates to a new trunkated time
            timeRemaining -= Time.deltaTime;
            float trunkTime = Mathf.Floor(timeRemaining * 100) / 100;
            timer.text = trunkTime.ToString();

            if (LeftWall.position.x <= endBox.transform.position.x - 15)
            {
                LeftWall.position = LeftWall.position + Vector3.right * horizontalMoveScalar * Time.deltaTime;
            }
            else
            {
                leftDone = true;
            }

            if (TopWall.position.y >= endBox.transform.position.y + 11)
            {
                TopWall.position = TopWall.position + Vector3.down * verticleMoveScalar * Time.deltaTime;//   (1/60) / 100
            }
            else topDone = true;


            if (RightWall.position.x >= endBox.transform.position.x + 15)
            {
                RightWall.position = RightWall.position + Vector3.left * horizontalMoveScalar * Time.deltaTime;
            }
            else rightDone = true;


            if (BottomWall.position.y <= endBox.transform.position.y - 11)
            {
                BottomWall.position = BottomWall.position + Vector3.up * verticleMoveScalar * Time.deltaTime;
            }
            else bottomDone = true;

        }
        else
        {
            Debug.Log("Timer Ended");
        }

        //Updating Health Bar
        CurrentHealth = Player.hp;
        healthImage.fillAmount = Player.hp / Player.maxHP;

        if (Application.isEditor)
        {
            //Camera Toggle
            if (Input.GetKeyDown(KeyCode.C))
            {
                //Scene Cam Toggle
                sceneCam.GetComponent<AudioListener>().enabled = !sceneCam.GetComponent<AudioListener>().enabled;
                sceneCam.enabled = !sceneCam.enabled;

                //Player Cam Toggle
                playerCam.GetComponent<AudioListener>().enabled = !playerCam.GetComponent<AudioListener>().enabled;
                playerCam.enabled = !playerCam.enabled;
            }
            //Sets time scale to 1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1;
            }
            //Doubles time scale
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale *= 2;
            }

            //Press H to heal
            if (Input.GetKeyDown(KeyCode.H))
            {
                Player.Heal(2);
            }
            //Press L to remove health
            if (Input.GetKeyDown(KeyCode.L))
            {
                Player.TakeDamage(2);
            }
        }
        //print(wallScale/Time.time);
    }

}
