using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHP;
    public float hp;
    public int speed;
    public int jumpHeight;
    Rigidbody2D bod;
    public bool isJumping;
    public Rigidbody2D platform;
    public GameObject cont;
    public Text healthText;
    [Space]
    public GameObject bullet;
    public Transform firePoint;
    public Vector3 dir;
    public int knockbackScalar;
    [Space]
    public List<Item> Inventory;
    public GameObject deathScene;

    private void Awake()
    {
        hp = maxHP;
        bod = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        healthText.text = hp.ToString() + "/" + maxHP.ToString();

        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = dir - firePoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        firePoint.rotation = Quaternion.Euler(0,0,angle);
        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        //WAD movement
        if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && !isJumping)
        {
            bod.AddForce(Vector2.up * jumpHeight);
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            bod.AddForce(Vector2.left * speed * Time.deltaTime);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Health"))
        {
            Inventory.Add(collision.GetComponent<Item>());
            maxHP += Inventory[Inventory.Count - 1].hpMod;
            Heal(Inventory[Inventory.Count - 1].hpMod);
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        for (int i = 0; i < damage; i++){
                hp--;
                if (hp<= 0) Die();
        }
    }

    public void knockback(GameObject obj)
    { 
        Vector2 knockback = transform.position - obj.transform.position;

        bod.AddForce(knockback * knockbackScalar);
    }

    public void Heal(float amt)
    {
        float newHP = hp + amt;
        if (newHP > maxHP) hp = maxHP;
        else hp = newHP;
    }

    private void Die()
    {
        deathScene.SetActive(true);
        gameObject.SetActive(false);
    }
    
}