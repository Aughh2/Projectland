using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    public Collider2D weaponCollider;

    [Header("Weapon Damage and Knockback")]
    public float knifeDamage = 20f;
    public float knifeKnockback = 2f;
    public float batDamage = 10f;
    public float batKnockback = 5f;

    private bool isAttacking = false;
    private readonly int[] meleeWeaponTypes = { 1, 2 };

    void Update()
    {
        int weaponType = animator.GetInteger("WeaponType");

        if (IsMeleeWeapon(weaponType) && Input.GetMouseButtonDown(0) && !isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            Debug.Log("Attack triggered");
        }
    }

    public void LockAttack()
    {
        Debug.Log("LockAttack triggered: Enabling collider");
        weaponCollider.enabled = true;

        Collider2D[] hits = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        int hitCount = weaponCollider.Overlap(filter, hits);

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D hit = hits[i];
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log($"Immediate hit detected with {hit.name}");
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    float damage = (animator.GetInteger("WeaponType") == 1) ? knifeDamage : batDamage;
                    float knockback = (animator.GetInteger("WeaponType") == 1) ? knifeKnockback : batKnockback;
                    enemy.TakeDamage(damage);
                    ApplyKnockback(hit.transform, knockback);
                }
            }
        }
    }

    public void UnlockAttack()
    {
        Debug.Log("UnlockAttack triggered: Disabling collider");
        weaponCollider.enabled = false;
        isAttacking = false;
    }

    private void ApplyKnockback(Transform enemyTransform, float knockbackForce)
    {
        Rigidbody2D enemyRb = enemyTransform.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            Vector2 direction = (enemyTransform.position - transform.position).normalized;
            enemyRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            Debug.Log($"Knockback applied to {enemyTransform.name}");
        }
    }

    private bool IsMeleeWeapon(int weaponType)
    {
        return System.Array.Exists(meleeWeaponTypes, type => type == weaponType);
    }
}
