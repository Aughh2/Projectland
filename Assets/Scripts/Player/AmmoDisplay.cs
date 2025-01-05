using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public ProjectileWeaponAttack weaponAttack;

    void Update()
    {
        int weaponType = weaponAttack.animator.GetInteger("WeaponType");

        if (weaponType == 3)
        {
            ammoText.text = $"Gun Ammo: {weaponAttack.gunCurrentMagazineAmmo}/{weaponAttack.gunTotalAmmo}";
        }
        else if (weaponType == 4)
        {
            ammoText.text = $"Rifle Ammo: {weaponAttack.rifleCurrentMagazineAmmo}/{weaponAttack.rifleTotalAmmo}";
        }
        else
        {
            ammoText.text = "";
        }
    }
}
