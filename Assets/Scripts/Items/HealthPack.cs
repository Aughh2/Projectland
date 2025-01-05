using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Debug.Log($"Player healed by {healAmount}!");
                Destroy(gameObject); 
            }
        }
    }
}
