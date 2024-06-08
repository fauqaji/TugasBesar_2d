using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffer : MonoBehaviour
{
    // Reference to the Weapon script
    private weapon weaponScript;
    private Animator animator;
    private bool isTaken = false; // Flag to ensure damage is added only once

    void Start()
    {
        weaponScript = FindObjectOfType<weapon>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Player" and if the item has not been taken yet
        if (other.CompareTag("Player") && !isTaken)
        {
            isTaken = true; // Set flag to true to prevent further increments
            Debug.Log("Collided with Player");
            // Add damage +7
            if (weaponScript != null)
            {
                Debug.Log("Weapon script found, adding damage.");
                weaponScript.damage += 7;
                animator.SetTrigger("taken");
            }
        }
        else
        {
            Debug.Log("Collided with something else: " + other.tag);
        }
    }


    public void BufferDestroyed()
    {
        Destroy(gameObject);
    }
}
