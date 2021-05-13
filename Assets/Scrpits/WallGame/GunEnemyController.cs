using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyController : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D bod;
    private GameController cont;
    public GameObject bullet;
    [Space]

    public int maxHp;
    public int hp;
    public int dmg;

    [Space]
    private Vector3 dir;
    public float dist;
    public float rotSpd;
    public float spd;
    public Vector2 vel;
    public float maxVel;
    [Space]
    public int timer;
    public int interval;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        cont = FindObjectOfType<GameController>();
        bod = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    private void Update()
    {
        vel = bod.velocity;
        timer++;
        dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotSpd);
        //Velocity Cap
        if (bod.velocity.x >= maxVel)
        {
            bod.velocity = new Vector2(maxVel, bod.velocity.y);
        }

        if (bod.velocity.y >= maxVel)
        {
            bod.velocity = new Vector2(bod.velocity.x, maxVel);
        }

        if (-bod.velocity.x >= maxVel)
        {
            bod.velocity = new Vector2(-maxVel, bod.velocity.y);
        }

        if (-bod.velocity.y >= maxVel)
        {
            bod.velocity = new Vector2(bod.velocity.x, -maxVel);
        }
        //Moving at a specific distance
        if (Mathf.Sqrt((dir.x * dir.x) + (dir.y * dir.y)) > dist)
        {
            bod.AddForce(transform.up * spd * Time.deltaTime);
        } 
        else
        {
            bod.AddForce(-1 * transform.up * spd * Time.deltaTime);
        }

        if(timer % interval == 0)
        {
            Shoot();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(collision.GetComponent<BulletScript>().dmg);
        }
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(dmg);
            player.knockback(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
            cont.GiveMoney(maxHp * 10);
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

}
