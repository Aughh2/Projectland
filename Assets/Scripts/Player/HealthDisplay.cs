using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText; 
    public PlayerHealth playerHealth; 

    void Update()
    {
        healthText.text = $"Health: {playerHealth.currentHealth}";
    }
}
