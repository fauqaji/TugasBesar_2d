using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider healthSlider;
    public GameObject healthPanel;
    public Transform enemyTransform; // Reference to the enemy's transform
    public Vector3 offset; // Offset from the enemy's position to the health bar's position

    

    public void UpdateSlimeHealth(EnemyHealth slimeHealthManager)
    {
        if (healthPanel != null)
        {
            healthSlider.maxValue = slimeHealthManager.GetMaxHealth();
            healthSlider.value = slimeHealthManager.GetCurrentHealth();
            healthPanel.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (enemyTransform != null)
        {
            // Update health panel position based on enemy's transform and offset
            Vector3 healthBarPosition = enemyTransform.position + offset;
            healthPanel.transform.position = healthBarPosition;

            // Check if the enemy is flipped and adjust the health bar position accordingly
            if (enemyTransform.localScale.x < 0)
            {
                healthPanel.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                healthPanel.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        // Hide the health panel if there is no slime currently being updated
        if (healthSlider.value <= 0)
        {
            healthPanel.SetActive(false);
        }
    }
}
