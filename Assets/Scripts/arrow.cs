using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public int damage;

    void Start()
    {
        rb.velocity = transform.up * speed; // Using transform.up for 2D
        Destroy(gameObject, 3f);
    }


    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallL1") || collision.gameObject.CompareTag("WallL2") || collision.gameObject.CompareTag("WallL3"))
        {
            Destroy(gameObject); // Destroy arrow gameObject
        }
        
    }
}
