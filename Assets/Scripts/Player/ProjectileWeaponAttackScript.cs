using UnityEngine;

public class ProjectileWeaponAttack : MonoBehaviour
{
    public Animator animator;

    public GameObject gunBulletPrefab;
    public GameObject rifleBulletPrefab;

    public Transform firePoint;

    public float gunFireRate = 0.5f;
    public float rifleFireRate = 0.2f;

    private float nextFireTime = 0f;

    public int gunMaxMagazineAmmo = 10;
    public int gunTotalAmmo = 30;
    public int gunCurrentMagazineAmmo;

    public int rifleMaxMagazineAmmo = 30;
    public int rifleTotalAmmo = 90;
    public int rifleCurrentMagazineAmmo;

    void Start()
    {
        gunCurrentMagazineAmmo = gunMaxMagazineAmmo;
        rifleCurrentMagazineAmmo = rifleMaxMagazineAmmo;
    }

    void Update()
    {
        int weaponType = animator.GetInteger("WeaponType");

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (weaponType == 3 && gunCurrentMagazineAmmo > 0) //Gun
            {
                FireProjectile(gunBulletPrefab, gunFireRate);
                gunCurrentMagazineAmmo--;
                TriggerAttackAnimation();
            }
            else if (weaponType == 4 && rifleCurrentMagazineAmmo > 0) //Rifle
            {
                FireProjectile(rifleBulletPrefab, rifleFireRate);
                rifleCurrentMagazineAmmo--;
                TriggerAttackAnimation();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon(weaponType);
        }
    }

    private void FireProjectile(GameObject projectilePrefab, float fireRate)
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = firePoint.position;
        Vector2 direction = (cursorPosition - (Vector3)firePointPosition).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        Bullet bulletScript = projectile.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(direction);
        }

        nextFireTime = Time.time + fireRate;
    }

    private void ReloadWeapon(int weaponType)
    {
        if (weaponType == 3)
        {
            int ammoNeeded = gunMaxMagazineAmmo - gunCurrentMagazineAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, gunTotalAmmo);

            gunCurrentMagazineAmmo += ammoToReload;
            gunTotalAmmo -= ammoToReload;
        }
        else if (weaponType == 4)
        {
            int ammoNeeded = rifleMaxMagazineAmmo - rifleCurrentMagazineAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, rifleTotalAmmo);

            rifleCurrentMagazineAmmo += ammoToReload;
            rifleTotalAmmo -= ammoToReload;
        }

        Debug.Log($"Weapon {weaponType} reloaded!");
    }

    private void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
