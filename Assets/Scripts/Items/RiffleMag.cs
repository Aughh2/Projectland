using UnityEngine;

public class RifleMag : MonoBehaviour
{
    public int ammoAmount = 40;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProjectileWeaponAttack weaponAttack = collision.GetComponent<ProjectileWeaponAttack>();
            if (weaponAttack != null)
            {
                weaponAttack.rifleTotalAmmo += ammoAmount; 
                Debug.Log($"Picked up {ammoAmount} rifle ammo!");
                Destroy(gameObject);
            }
        }
    }
}
