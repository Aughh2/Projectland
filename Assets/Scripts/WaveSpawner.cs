using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 5;
    public float waveDifficultyMultiplier = 1.2f;

    [Header("UI References")]
    public TextMeshProUGUI waveText;
    public GameObject gameOverUI;
    public TextMeshProUGUI gameOverText;

    private int currentWave = 0;
    private float survivalTime = 0f;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool isGameOver = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
        }
    }

    private IEnumerator StartWave()
    {
        while (!isGameOver)
        {
            while (activeEnemies.Count > 0)
            {
                activeEnemies.RemoveAll(enemy => enemy == null);
                yield return null; 
            }

            currentWave++;
            UpdateWaveUI();

            int enemyCount = Mathf.CeilToInt(enemiesPerWave * Mathf.Pow(waveDifficultyMultiplier, currentWave - 1));
            SpawnEnemies(enemyCount);

            yield return null;
        }
    }

    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);

            GameObject enemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
    }

    private void UpdateWaveUI()
    {
        waveText.text = $"Wave: {currentWave}";
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);


        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);

        gameOverText.text = $"Game Over\nSurvived Time: \n{minutes}m {seconds}s\nWaves Completed: \n{currentWave}";

        StopAllCoroutines();
    }
}
