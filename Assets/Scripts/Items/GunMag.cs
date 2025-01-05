using UnityEngine;

public class GunMag : MonoBehaviour
{
    public int ammoAmount = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProjectileWeaponAttack weaponAttack = collision.GetComponent<ProjectileWeaponAttack>();
            if (weaponAttack != null)
            {
                weaponAttack.gunTotalAmmo += ammoAmount;
                Debug.Log($"Picked up {ammoAmount} gun ammo!");
                Destroy(gameObject);
            }
        }
    }
}
