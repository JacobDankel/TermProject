using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{

    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth = 100f;
    PlayerController Player; 
    // Start is called before the first frame update
    void Start()
    {
        //To Find Health
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>(); 

    }

    // Update is called once per frame
    void Update()
    {
        //Determines the percentage at which the player is at (For Slider) 
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
}
