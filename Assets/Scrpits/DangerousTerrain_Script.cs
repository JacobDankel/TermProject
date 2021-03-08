using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousTerrain_Script : MonoBehaviour
{

    //Creating a Reusable collider that Damages player when they collide with anything under the DangerousTerrain_Script
    //Trying not to define variables with specific numbers for maximum reusability
    public int damage;
    Rigidbody2D Bod;
    bool hasHurt;

    // Start is called before the first frame update

    private void Awake()
    {
        Bod = GetComponent<Rigidbody2D>(); 

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHurt)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            hasHurt = true;
            Invoke("Disable", 0.001f);
        }
    }
