using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeRemaining;
    public Text timer;

    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float trunkTime = Mathf.Floor(timeRemaining * 100) / 100;
            timer.text = trunkTime.ToString();

        }
        else
        {
            Debug.Log("Timer Ended");
        }
    }
}
