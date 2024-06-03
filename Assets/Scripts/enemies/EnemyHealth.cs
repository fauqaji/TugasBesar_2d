using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private SlimeController slimeController;

    void Start()
    {
        currentHealth = maxHealth;
        slimeController = GetComponent<SlimeController>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        slimeController.TakeDamage();
        // Beritahu UIManager untuk memperbarui health bar
        EnemyHealthBar.Instance.UpdateSlimeHealth(this);


        if (currentHealth <= 0)
        {
            slimeController.Die();

            if (currentHealth > 0)
            {
                slimeController.ShowDamageEffect();
            }
            else if (currentHealth <= 0)
            {
                slimeController.Die();

            }
        }
    }
        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }


    
}
