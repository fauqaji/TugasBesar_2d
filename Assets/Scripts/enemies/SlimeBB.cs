using UnityEngine;

public class SlimeBB : MonoBehaviour
{
    public float moveRange = 1f; // Range of the movement

    private Vector2 startingPosition;
    private bool movingRight = true;

    private Animator animator;

    void Start()
    {
        startingPosition = transform.position;
        animator = GetComponent<Animator>();
        animator.Play("slime-move");
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime);

            if (transform.position.x >= startingPosition.x + moveRange)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime);

            if (transform.position.x <= startingPosition.x - moveRange)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
