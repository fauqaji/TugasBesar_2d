using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private Rigidbody2D rb;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 dir = Vector2.zero;
            bool mov = false;
            bool movKa = false;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetBool("kiri", true);
                animator.SetInteger("Direction", 3);
                mov = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetBool("kanan", true);
                animator.SetInteger("Direction", 2);
                mov = true;
            }
            else
            {
                animator.SetBool("kiri", false);
                animator.SetBool("kanan", false);
                animator.SetBool("kananIdle", true);
                animator.SetBool("kiriIdle", true);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetBool("atas", true);
                animator.SetInteger("Direction", 1);
                mov = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetBool("bawah", true); // Set animasi "bawah" ke true
                animator.SetInteger("Direction", 0);
                mov = true;
                
            }
            else
            {
                animator.SetBool("atas", false);
                animator.SetBool("bawah", false);
                animator.SetBool("atasIdle", true);

            }

            animator.SetBool("mov", mov);
            dir.Normalize();
            rb.velocity = speed * dir;
        }
    }
}
