using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHP;
    public float hp;
    public int speed;
    public int jumpHeight;
    Rigidbody2D bod;
    public bool isJumping;
    public Rigidbody2D platform;

    private void Awake()
    {
        hp = maxHP;
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
            if (collision.gameObject.CompareTag("Platform"))
            {
                platform = collision.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }



    public void TakeDamage(int damage)
    {
        for (int i = 0; i < damage; i++){
                hp--;
                if (hp<= 0) Die();
        }
    }

    public void Heal(float amt)
    {
        float newHP = hp + amt;
        if (newHP > maxHP) hp = maxHP;
        else hp = newHP;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0.5f;
    }
    
}