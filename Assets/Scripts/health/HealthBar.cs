using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth;
    public Gradient gradient;
    public Image fill;
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
