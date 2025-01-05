using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Animator animator;

    public AnimatorOverrideController baseController;
    public AnimatorOverrideController batController;
    public AnimatorOverrideController knifeController;
    public AnimatorOverrideController gunController;
    public AnimatorOverrideController riffleController;
    public AnimatorOverrideController flamethrowerController;

    void Start()
    {
        EquipWeapon(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipWeapon(5);
        }
    }

    public void EquipWeapon(int weaponType)
    {
        switch (weaponType)
        {
            case 1:
                animator.runtimeAnimatorController = knifeController;
                animator.SetInteger("WeaponType", 1);
                break;
            case 2:
                animator.runtimeAnimatorController = batController;
                animator.SetInteger("WeaponType", 2);
                break;
            case 3:
                animator.runtimeAnimatorController = gunController;
                animator.SetInteger("WeaponType", 3);
                break;
            case 4:
                animator.runtimeAnimatorController = riffleController;
                animator.SetInteger("WeaponType", 4);
                break;
            case 5:
                animator.runtimeAnimatorController = flamethrowerController;
                animator.SetInteger("WeaponType", 5);
                break;
            default:
                animator.runtimeAnimatorController = knifeController;
                animator.SetInteger("WeaponType", 1);
                break;
        }
    }
}
