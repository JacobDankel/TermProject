using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float spd;
    Rigidbody2D bod;
    public int dmg = 1;
    public int pierce;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        bod.AddForce(transform.up * spd);

        Invoke("Disable", 2f);
    }

    public void addDmg(int n)
    {
        dmg += n;
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
