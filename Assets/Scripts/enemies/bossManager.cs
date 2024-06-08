using UnityEngine;

public class bossManager : MonoBehaviour
{
    public GameObject boss; // Drag and drop game object boss ke sini di Inspector
    private int enemyCount;

    void OnEnable()
    {
        // Subscribe ke event ketika musuh mati
        EnemyHealth.OnEnemyDeath += OnEnemyDeath;
    }

    void OnDisable()
    {
        // Unsubscribe dari event ketika musuh mati
        EnemyHealth.OnEnemyDeath -= OnEnemyDeath;
    }

    void Start()
    {
        // Hitung jumlah musuh dengan tag "Enemy" di scene
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        boss.SetActive(false); // Pastikan boss tidak aktif di awal
    }

    void OnEnemyDeath()
    {
        enemyCount--;

        // Jika semua musuh telah mati, munculkan boss
        if (enemyCount <= 0)
        {
            boss.SetActive(true);
        }
    }
}
