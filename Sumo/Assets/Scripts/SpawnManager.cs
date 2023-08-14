using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public GameObject[] powerupPrefabs;

    public PlayerController player;
    private Rigidbody playerRb;

    private float spawnRange = 9;
    private int powerupRange;
    public int enemyCount;
    public int waveNumber = 1;
    public int levelLoader = 1;

    public bool powerUpUsed;

    void Start()
    {
        powerupRange = player.numPowers;
        SpawnWave(waveNumber);
        spawnPowerUp();
        playerRb = playerPrefab.GetComponent<Rigidbody>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;   
        if (enemyCount == 0 )
        {
            if (levelLoader > 4) {
                SceneManager.LoadScene("Level 2");
            }
            else { 
            waveNumber++;
            levelLoader++;
            SpawnWave(waveNumber);
            spawnPowerUp();
            }
        }
    }

    private Vector3 GenerateSpawnPos ()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnRangeX, 0.1f, spawnRangeZ);
        return randomPos;
    }

    void SpawnWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++) {
        Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    void spawnPowerUp()
    {
        powerUpUsed = player.wavePUsed;
        if (powerUpUsed) { 
            int randomPowerup = Random.Range(0, powerupRange);
            GameObject activeGameObject = powerupPrefabs[randomPowerup];
            activeGameObject.transform.position = GenerateSpawnPos();
            activeGameObject.GetComponent<MeshRenderer>().enabled = true;
            player.wavePUsed = false;
        }
    }
}
