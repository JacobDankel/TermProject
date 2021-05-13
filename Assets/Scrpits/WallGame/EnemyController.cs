using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;

    public int maxHp;
    public int hp;
    [Space]
    public int dmg;
    public float spd;
    public Vector2 maxSpd;
    [Space]
    public float rotSpd;
    public float distance;
    Vector3 dir;
    [Space]
    Rigidbody2D bod;
    GameController cont;
    public Transform target;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        cont = FindObjectOfType<GameController>();
        bod = GetComponent<Rigidbody2D>();
        hp = maxHp;
        target = cont.Player.transform;
    }

    void Update()
    {
        dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotSpd);

        bod.AddForce(transform.up * spd * Time.deltaTime) ;

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
        if(hp <= 0)
        {
            Destroy(gameObject);
            cont.GiveMoney(maxHp * 10);
        }
    }
}
