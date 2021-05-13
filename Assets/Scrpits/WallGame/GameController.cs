using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController Player;
    private Vector3 playerSpawn;
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

    public List<Item> items;
    public GameObject[] itemSpawns;
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
    private float xPos = 34;
    private float yPos = 21;
    [Space]
    public GameObject endBox;
    public float endBoxVertBound;
    public float endBoxHorzBound;
    [Space]
    public int roundNum = 1;
    public List<GameObject> enemies;
    public List<GameObject> enemiesSpawned;
    public List<Transform> spawnpoints;
    public float enemySpawnRateFast;
    private int fastMod = 10;
    public float enemySpawnRateSlow;
    private int slowMod = 30;
    private bool spawnEnemies;
    private bool endRound;

    private void Start()
    {
        endRound = false;
        CurrentHealth = Player.hp;
        timeRemaining = timeLimit;
        currentCamera = playerCam;
        playerSpawn = new Vector3(0, -2, 0);

        //Populate item spawn position
        for(int i = 0; i < itemSpawns.Length; i++)
        {
            itemSpawns[i] = GameObject.FindGameObjectWithTag("Platform");
            itemSpawns[i].SetActive(false);
        }
        for (int i = 0; i < itemSpawns.Length; i++) //Extra Loop to properly add the spawns in the array
        {
            itemSpawns[i].SetActive(true);
        }

        //Randomizes the box position
        randBoxPos();

        //Calcuates the amount to move the walls
        topVerticleMoveScalar = 0.25f;
        bottomVerticleMoveScalar = 0.25f;
        leftHorizontalMoveScalar = 0.5f;
        rightHorizontalMoveScalar = 0.5f;
        

        playerCam.enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = true;

        spawnEnemies = true;

        spawnItems();
        spawnItems();
    }

    private void FixedUpdate()
    {
        //Fast Enemy Spawn
        enemySpawnRateFast = timeRemaining % fastMod;
        if (enemySpawnRateFast <= 0.1 && spawnEnemies)
        {
            Debug.Log("Fast Spawn");
            for(int i = 0; i < 1; i++)
            {
                GameObject enemy = Instantiate(enemies[0], spawnpoints[Random.Range(0,20)]);
                enemiesSpawned.Add(enemy);
            }
            fastMod = Random.Range(7, 11);
        }
        //Slow Enemy Spawn
        enemySpawnRateSlow = timeRemaining % slowMod;
        if (enemySpawnRateSlow <= 0.1 && spawnEnemies)
        {
            Debug.Log("Slow Spawn");
            for (int i = 0; i < 3; i++)
            {
                GameObject enemy1 = Instantiate(enemies[0], spawnpoints[Random.Range(0, 20)]);
                GameObject enemy2 = Instantiate(enemies[1], spawnpoints[Random.Range(0, 20)]);
                enemiesSpawned.Add(enemy1);
                enemiesSpawned.Add(enemy2);

            }
            slowMod = Random.Range(28, 35);
        }
    }

    public void Update()
    {
        //Timer
        if(timeRemaining <= timeLimit - 20 && !startWalls)      //Starts the walls 20 seconds in
        {
            startWalls = true;
        }

        //Time Counter & Text Updates
        if (!endRound)
        {
            timeRemaining -= Time.deltaTime;
            float trunkTime = Mathf.Floor(timeRemaining * 100) / 100;
            timer.text = trunkTime.ToString();
        }

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

            if (TopWall.position.y >= endBox.transform.position.y + 12.5)
            {
                TopWall.position = TopWall.position + Vector3.down * topVerticleMoveScalar * Time.deltaTime;//   (1/60) / 100
            }
            else topDone = true;


            if (RightWall.position.x >= endBox.transform.position.x + 16)
            {
                RightWall.position = RightWall.position + Vector3.left * rightHorizontalMoveScalar * Time.deltaTime;
            }
            else rightDone = true;


            if (BottomWall.position.y <= endBox.transform.position.y - 12.5)
            {
                BottomWall.position = BottomWall.position + Vector3.up * bottomVerticleMoveScalar * Time.deltaTime;
            }
            else bottomDone = true;

        }

        //--------End of the Timer--------//
        if(timeRemaining <= 0)
        {
            Debug.Log("Timer Ended");
            spawnEnemies = false;
            endRound = true;
            Respawn();
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

    public void spawnItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Vector3 spot = itemSpawns[Random.Range(0,41)].transform.position;
            spot.y += 1;
            Instantiate(items[i], spot, Quaternion.identity);
        }
    }

    public void GiveMoney(int num)
    {
        money += num;
        moneytxt.text = money.ToString();
    }

    //End of Timer Reset
    public void Respawn()
    {
        for(int i = 0; i < spawnpoints.Count; i++)
        {
            spawnpoints[i].gameObject.SetActive(false);
        }
        Player.transform.position = playerSpawn;
        timeRemaining = timeLimit;
        LeftWall.transform.position = new Vector3(-xPos, 0, 0);
        RightWall.transform.position = new Vector3(xPos, 0, 0);
        TopWall.transform.position = new Vector3(0, yPos, 0);
        BottomWall.transform.position = new Vector3(0, -yPos, 0);
        endRound = false;
        startWalls = false;
        for (int i = 0; i < spawnpoints.Count; i++)
        {
            spawnpoints[i].gameObject.SetActive(true);
        }
        for(int i = 0; i < enemiesSpawned.Count; i++)
        {
            Destroy(enemiesSpawned[i]);
        }

        spawnItems();
        spawnItems();
    }

    public void randBoxPos()
    {
        endBoxHorzBound = Random.Range(-15f, 15f);      // Net Range of 30 Units
        endBoxVertBound = Random.Range(-8f, 8f);        // Net Range of 16 Units
        endBox.transform.position = new Vector3(endBoxHorzBound, endBoxVertBound, 0);
    }
}
