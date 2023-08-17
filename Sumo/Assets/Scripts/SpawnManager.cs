using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;
    public int powerupRange;
    public int enemyCount;

    public bool powerUpUsed;

    private int randomPowerup;

    public GameObject[] powerupPrefabs;

    public GameManager gameManager;

    public Vector3 GenerateSpawnPos ()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnRangeX, 0.1f, spawnRangeZ);
        return randomPos;
    }

    public void spawnPowerUp()
    {
        if (gameManager.isGameActive) { 
            int lastRandom = randomPowerup;
            randomPowerup = Random.Range(0, powerupRange);
        if (randomPowerup == lastRandom)
        {
            randomPowerup++;
            randomPowerup %= powerupRange;
        }
        GameObject activeGameObject = powerupPrefabs[randomPowerup];
        activeGameObject.transform.position = GenerateSpawnPos();
        activeGameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    int Mod(int a, int n)
    {
        return ((a % n) + n) % n;
    }
}

