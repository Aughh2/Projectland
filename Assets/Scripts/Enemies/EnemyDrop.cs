using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Drop Prefabs")]
    public GameObject gunMagPrefab;
    public GameObject rifleMagPrefab;
    public GameObject healthPackPrefab;

    [Header("Drop Chances")]
    public float overallDropChance = 0.2f;
    public float gunMagChance = 0.5f;
    public float rifleMagChance = 0.3f;
    public float healthPackChance = 0.4f;

    public void DropItem()
    {
        float randomValue = Random.value;

        if (randomValue > overallDropChance)
        {
            return; 
        }

        float itemRoll = Random.value;

        if (itemRoll <= gunMagChance && gunMagPrefab != null)
        {
            Instantiate(gunMagPrefab, transform.position, Quaternion.identity);
        }
        else if (itemRoll <= gunMagChance + rifleMagChance && rifleMagPrefab != null)
        {
            Instantiate(rifleMagPrefab, transform.position, Quaternion.identity);
        }
        else if (itemRoll <= gunMagChance + rifleMagChance + healthPackChance && healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        }
    }
}
