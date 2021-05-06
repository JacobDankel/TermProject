using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{

    private Text healthTxt;
    public float CurrentHealth;
    public float MaxHealth;
    PlayerController Player; 
    // Start is called before the first frame update
    void Start()
    {
        //To Find Health
        Player = FindObjectOfType<PlayerController>();
        healthTxt = FindObjectOfType<Text>();
        MaxHealth = Player.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //Determines the percentage at which the player is at (For Slider) 
        MaxHealth = Player.maxHP;
        CurrentHealth = Player.hp;
        healthTxt.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
    }
}
