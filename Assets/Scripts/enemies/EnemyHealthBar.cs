using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public static EnemyHealthBar Instance;

    public Slider healthSlider;
    public GameObject healthPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateSlimeHealth(EnemyHealth slimeHealthManager)
    {
        healthSlider.maxValue = slimeHealthManager.GetMaxHealth();
        healthSlider.value = slimeHealthManager.GetCurrentHealth();
        healthPanel.SetActive(true);
    }
}
