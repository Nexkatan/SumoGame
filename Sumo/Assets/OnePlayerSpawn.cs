using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnePlayerSpawn : SpawnManager
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public PlayerController player;

    
    public int levelLoader = 1;

    void Start()
    {
        SpawnWave(waveNumber);
        spawnPowerUp();
    }

    public void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            if (levelLoader > 4)
            {
                SceneManager.LoadScene("Level 2");
            }
            else
            {
                waveNumber++;
                gameManager.UpdateScore();
                levelLoader++;
                SpawnWave(waveNumber);
                OnePlayerSpawnPowerUp();
            }
        }
    }

    void SpawnWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    void OnePlayerSpawnPowerUp()
    {
        powerUpUsed = player.wavePUsed;
        if (powerUpUsed)
        {
            spawnPowerUp();
            player.wavePUsed = false;
        }
    }
}
