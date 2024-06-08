using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float returnDistance = 15f;
    public float moveSpeed = 3.5f;
    public float attackCooldown = 2f;
    public int attackDamage = 10;
    public float jumpForce = 5f;
    public float aoeRadius = 3f;  // Radius kecil untuk serangan AOE
    private EnemyHealth enemyHealth;
    public float flashDuration = 0.1f; // Durasi kedip
    public int flashCount = 5;
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private bool isDead = false;
    private bool isChasing = false;
    private float attackTimer = 0f;
    private Vector3 originalScale;
    private Vector2 targetPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        originalScale = transform.localScale;
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
                SlimeManager.Instance.RegisterChasingSlime(this);
            }
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                SlimeManager.Instance.UnregisterChasingSlime(this);
            }
            ReturnToInitialPosition();
        }

        attackTimer -= Time.deltaTime;

        // Ensure the slime doesn't rotate
        transform.rotation = Quaternion.identity;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        ResetAllTriggers();
        animator.SetTrigger("Move");

        // Flip sprite based on direction
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void AttackPlayer()
    {
        rb.velocity = Vector2.zero;
        if (attackTimer <= 0f)
        {
            targetPosition = player.position; // Save the player's position at the moment of attack
            ResetAllTriggers();
            animator.SetTrigger("Attack");
            attackTimer = attackCooldown;
            JumpToTarget();
        }

        // Flip sprite based on direction
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void JumpToTarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
    }

    void ReturnToInitialPosition()
    {
        Vector2 direction = (initialPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        ResetAllTriggers();
        animator.SetTrigger("Move");

        if (Vector2.Distance(transform.position, initialPosition) <= 0.5f)
        {
            Idle();
        }

        // Flip sprite based on direction
        if (initialPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void Idle()
    {
        rb.velocity = Vector2.zero;
        ResetAllTriggers();
        animator.SetTrigger("Idle");

        // Ensure the slime is facing the initial direction
        if (initialPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    public void TakeDamage()
    {
        ResetAllTriggers();
        animator.SetTrigger("Hurt");
        // Implement health reduction and check if slime should die
        // if health <= 0, call Die()
    }

    public void Die()
    {
        isDead = true;
        isChasing = false;
        SlimeManager.Instance.UnregisterChasingSlime(this);
        rb.velocity = Vector2.zero;
        ResetAllTriggers();
        animator.SetTrigger("Die");
        // Implement any additional death logic (e.g., remove from scene)
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Hurt");
        animator.ResetTrigger("Die");
    }

    // Called by the animation event
    public void OnAttackHit()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        foreach (Collider2D collider in hitPlayers)
        {
            if (collider != null && collider.CompareTag("Player"))
            {
                PlayerLife playerLife = collider.GetComponent<PlayerLife>();
                if (playerLife != null)
                {
                    playerLife.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogWarning("Player object does not have a PlayerLife component: " + collider.name);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            arrow bow = other.GetComponent<arrow>();
            if (bow != null)
            {
                enemyHealth.TakeDamage(bow.damage);
                Destroy(other.gameObject);
            }
        }
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    public void ShowDamageEffect()
    {
        StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        Color originalColor = spriteRenderer.color;
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
