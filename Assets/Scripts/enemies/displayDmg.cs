using UnityEngine;
using UnityEngine.UI;

public class displayDmg : MonoBehaviour
{
    public Text countDmg; // Reference to the UI Text element for damage

    // Method to display damage
    public void DisplayDamage(int damage)
    {
        countDmg.text = "" + damage.ToString();
    }
}
