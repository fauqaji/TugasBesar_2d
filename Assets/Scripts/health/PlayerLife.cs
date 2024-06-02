using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;
    private SpriteRenderer spriteRenderer; // Gunakan SpriteRenderer untuk game 2D
    public Color damageColor = Color.red; // Warna merah untuk efek kedip
    public float flashDuration = 0.1f; // Durasi kedip
    public GameObject endGamePanel;
    public int flashCount = 2; // Jumlah kedip

    void Start()
    {
        health = healthBar.maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Dapatkan SpriteRenderer dari player
    }

    void Update()
    {
        healthBar.SetHealth(health);
        if (health <= 0 && health != -1)
        {
            health = -1;
            StartCoroutine(HandleDeath());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
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

    private IEnumerator HandleDeath()
    {
        // Tambahkan efek kematian jika diperlukan
        yield return new WaitForSeconds(1f); // Waktu tunggu sebelum menghilangkan player
        Destroy(gameObject); // Menghilangkan player dari scene
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
    }
}
