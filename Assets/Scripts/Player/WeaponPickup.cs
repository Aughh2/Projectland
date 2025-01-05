using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponType;
    public int ammoAmount = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProjectileWeaponAttack weaponAttack = collision.GetComponent<ProjectileWeaponAttack>();
            if (weaponAttack != null)
            {
                if (weaponType == 3)
                {
                    weaponAttack.gunTotalAmmo += ammoAmount;
                }
                else if (weaponType == 4)
                {
                    weaponAttack.rifleTotalAmmo += ammoAmount;
                }

                Debug.Log($"Picked up ammo for weapon {weaponType}: {ammoAmount} added!");
                Destroy(gameObject);
            }
        }
    }
}
