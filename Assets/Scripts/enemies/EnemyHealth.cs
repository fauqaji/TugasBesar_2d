using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    public delegate void EnemyDeathEventHandler();
    public static event EnemyDeathEventHandler OnEnemyDeath;

    public Text countDmg;
    public Transform enemyTransform;
    public Vector3 offset;
    public float displayDuration = 0.8f;
    public float moveSpeed = 20f;

    public int maxHealth = 100;
    private int currentHealth;
    private SlimeController slimeController;
    public EnemyHealthBar enemyHealthBar;
    void Start()
    {
        currentHealth = maxHealth;
        slimeController = GetComponent<SlimeController>();
    }
    void Update()
    {
        UpdateCountDmgPosition();
    }
    public void TakeDamage(int damage)
    {
        countDmg.text = damage.ToString();
        countDmg.gameObject.SetActive(true);
        StartCoroutine(MoveTextUpward(displayDuration));

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        slimeController.TakeDamage();
        enemyHealthBar.UpdateSlimeHealth(this);
        if (currentHealth <= 0)
        {
            slimeController.Die();

            if (currentHealth > 0)
            {
                slimeController.ShowDamageEffect();
            }
            else if (currentHealth <= 0)
            {
                if (OnEnemyDeath != null)
                {
                    OnEnemyDeath();
                }
                slimeController.Die();

            }
        }
        UpdateCountDmgPosition();
    }
        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }


    public void UpdateCountDmgPosition()
    {
        if (enemyTransform != null)
        {
            // Update posisi text damage berdasarkan transform musuh dan offset
            Vector3 countDmgPosition = enemyTransform.position + offset;
            countDmg.transform.position = countDmgPosition;

            // Cek apakah musuh terbalik dan sesuaikan skala text damage sesuai
            if (enemyTransform.localScale.x < 0)
            {
                countDmg.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                countDmg.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    private IEnumerator MoveTextUpward(float duration)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = countDmg.transform.localPosition;

        while (elapsedTime < duration)
        {
            countDmg.transform.localPosition = originalPosition + Vector3.up * moveSpeed * elapsedTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        countDmg.gameObject.SetActive(false); // Nonaktifkan Text setelah animasi selesai
        countDmg.transform.localPosition = originalPosition; // Reset posisi Text
    }
}
