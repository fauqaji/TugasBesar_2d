using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 50;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed; // Menggunakan transform.up untuk 2D

        Destroy(gameObject, 3f);
    }

    
}
