using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health = 100f;
    public int speed;
    public int jumpHeight;
    Rigidbody2D bod;
    public bool isJumping;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && !isJumping)
        {
            bod.AddForce(Vector2.up * jumpHeight);
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            bod.AddForce(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //bod.AddForce(Vector2.down);
        }

        if (Input.GetKey(KeyCode.D))
        {
            bod.AddForce(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            isJumping = false;
        }
    }


    /*public void TakeDamage(int damage)
    {
        if (//Needs to add if player collides with terrain then take dmg;)
        {
            for (int i = 0; i < damage; i++)
            {
                Health--;


                if (Health <= 0) Die();
            }


        }
    }

        void Die()
        {
            gameObject.SetActive(false);
        }
    
*/
}