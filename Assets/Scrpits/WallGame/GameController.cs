﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController Player;
    public int money;
    public Text moneytxt;
    [Space]
    public Camera playerCam;
    public Camera currentCamera;
    public Color32 backgroundColor;
    public Cursor cursor;
    public AudioClip wallFinish;
    [Space]
    public float CurrentHealth;
    
    //Potential UI Item List

    [Space]
    public float timeLimit;
    public float timeRemaining;
    public Text timer;
    public bool startWalls = false;
    [Space]
    public float topVerticleMoveScalar;
    public float bottomVerticleMoveScalar;
    public float leftHorizontalMoveScalar;
    public float rightHorizontalMoveScalar;
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
    [Space]
    public int roundNum = 1;
    public List<GameObject> enemies;
    public List<Transform> spawnpoints;
    public float enemySpawnRateFast;
    private int fastMod = 10;
    public float enemySpawnRateSlow;
    private int slowMod = 30;

    private void Start()
    {
        CurrentHealth = Player.hp;
        timeRemaining = timeLimit;
        currentCamera = playerCam;

        //Randomizes the box position
        endBoxHorzBound = Random.Range(-15f, 15f);      // Net Range of 30 Units
        endBoxVertBound = Random.Range(-8f, 8f);        // Net Range of 16 Units
        endBox.transform.position = new Vector3(endBoxHorzBound, endBoxVertBound, 0);

        //Calcuates the amount to move the walls
        topVerticleMoveScalar = 0.25f;
        bottomVerticleMoveScalar = 0.25f;
        leftHorizontalMoveScalar = 0.5f;
        rightHorizontalMoveScalar = 0.5f;
        

        playerCam.enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = true;

    }

    private void FixedUpdate()
    {
        //Fast Enemy Spawn
        enemySpawnRateFast = timeRemaining % fastMod;
        if (enemySpawnRateFast <= 0.1)
        {
            for(int i = 0; i < 1; i++)
            {
                Instantiate(enemies[i], spawnpoints[Random.Range(0,20)]);
            }
            fastMod = Random.Range(7, 11);
        }
        //Slow Enemy Spawn
        enemySpawnRateSlow = timeRemaining % slowMod;
        if (enemySpawnRateSlow == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemies[0], spawnpoints[Random.Range(0, 20)]);
            }
            slowMod = Random.Range(28, 35);
        }
    }

    public void Update()
    {
        //Timer
        
        if(timeRemaining <= timeLimit - 20 && !startWalls)
        {
            Debug.Log(timeRemaining);
            startWalls = true;
        }

        timeRemaining -= Time.deltaTime;
        float trunkTime = Mathf.Floor(timeRemaining * 100) / 100;
        timer.text = trunkTime.ToString();

        if (timeRemaining > 0 && startWalls)
        {
            //Reduces the time and also updates to a new trunkated time

            if (LeftWall.position.x <= endBox.transform.position.x - 16)
            {
                LeftWall.position = LeftWall.position + Vector3.right * leftHorizontalMoveScalar * Time.deltaTime;
            }
            else
            {
                leftDone = true;
            }

            if (TopWall.position.y >= endBox.transform.position.y + 12)
            {
                TopWall.position = TopWall.position + Vector3.down * topVerticleMoveScalar * Time.deltaTime;//   (1/60) / 100
            }
            else topDone = true;


            if (RightWall.position.x >= endBox.transform.position.x + 16)
            {
                RightWall.position = RightWall.position + Vector3.left * rightHorizontalMoveScalar * Time.deltaTime;
            }
            else rightDone = true;


            if (BottomWall.position.y <= endBox.transform.position.y - 12)
            {
                BottomWall.position = BottomWall.position + Vector3.up * bottomVerticleMoveScalar * Time.deltaTime;
            }
            else bottomDone = true;

        }
        if(timeRemaining <= 0)
        {
            Debug.Log("Timer Ended");
        }

        if (Application.isEditor)
        {
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

    public void GiveMoney(int num)
    {
        money += num;
        moneytxt.text = money.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene("WallGame");
    }
    
    public void Quit()
    {
        SceneManager.LoadScene("WallGameTitleScene");
    }
}
