using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        public Transform aim; // Tambahkan ini

        private Animator animator;
        private Rigidbody2D rb;
        private bool isAttacking = false;
        private bool facingRight = true;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (isAttacking)
            {


                if (currentState.normalizedTime >= 1.0f)
                {
                    if (currentState.IsName("attackHorizontal"))
                    {
                        isAttacking = false;
                        animator.SetTrigger("horizontalIdle");
                    }
                    else if (currentState.IsName("attackBawah"))
                        
                    {

                        isAttacking = false;
                        animator.SetTrigger("bawahIdle");
                    }
                    else if (currentState.IsName("attackAtas"))
                    {
                        isAttacking = false;
                        animator.SetTrigger("atasIdle");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }


            Vector2 movement = Vector2.zero;
            bool isMoving = false;

            animator.SetBool("kiri", false);
            animator.SetBool("kanan", false);
            animator.SetBool("atas", false);
            animator.SetBool("bawah", false);

            if (Input.GetKey(KeyCode.A))
            {
                movement.x = -1;
                if (facingRight)
                {
                    Flip();
                }
                animator.SetBool("horizontal", true);
                isMoving = true;

                aim.position = transform.position + new Vector3(-1, 0, 0); // Update aim position
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement.x = 1;
                if (!facingRight)
                {
                    Flip();
                }

                animator.SetBool("horizontal", true);
                isMoving = true;

                aim.position = transform.position + new Vector3(1, 0, 0); // Update aim position
            }
            else
            {
                animator.SetBool("horizontal", false);
            }

            if (Input.GetKey(KeyCode.W))
            {
                movement.y = 1;
                animator.SetBool("atas", true);
                isMoving = true;

                aim.position = transform.position + new Vector3(0, 1, 0); // Update aim position
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement.y = -1;
                animator.SetBool("bawah", true);
                isMoving = true;

                aim.position = transform.position + new Vector3(0, -1, 0); // Update aim position
            }
            else if (!isMoving)
            {
                animator.SetBool("bawah", false);
                animator.SetBool("atas", false);
            }

            
            if (Input.GetMouseButtonDown(0))
            {
                if (currentState.IsName("horizontalIdle"))
                {
                    resetAllTriggers();
                    animator.SetTrigger("attackHorizontal");
                    isAttacking = true;
                }
                else if (currentState.IsName("idle"))
                {
                    resetAllTriggers();
                    animator.SetTrigger("attackBawah");
                    isAttacking = true;
                }
                else if (currentState.IsName("atasIdle"))
                {
                    resetAllTriggers();
                    animator.SetTrigger("attackAtas");
                    isAttacking = true;
                }
            }

            animator.SetBool("mov", isMoving);
            movement.Normalize();
            rb.velocity = speed * movement;
        }

        private void resetAllTriggers()
        {
            animator.ResetTrigger("attackHorizontal");
            animator.ResetTrigger("attackBawah");
            animator.ResetTrigger("attackAtas");

            animator.ResetTrigger("horizontalIdle");
            animator.ResetTrigger("bawahIdle");
            animator.ResetTrigger("atasIdle");
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
