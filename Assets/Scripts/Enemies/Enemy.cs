using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        Debug.Log($"Enemy took {damage} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("Die"); 
        GetComponent<EnemyAI>().enabled = false; 
        GetComponent<Collider2D>().enabled = false;
        EnemyDrop dropComponent = GetComponent<EnemyDrop>();
        if (dropComponent != null)
        {
            dropComponent.DropItem();
        }
        Destroy(gameObject, 3f); 
    }
}
