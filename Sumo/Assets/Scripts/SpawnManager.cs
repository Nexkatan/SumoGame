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

    public Vector3 GenerateSpawnPos ()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnRangeX, 0.1f, spawnRangeZ);
        return randomPos;
    }

    public void spawnPowerUp()
    {
            int lastRandom = randomPowerup;
            randomPowerup = Random.Range(0, powerupRange);
        Debug.Log(lastRandom);
        Debug.Log(randomPowerup);
        if (randomPowerup == lastRandom) 
        {
            if (randomPowerup == powerupRange)
            {
                randomPowerup--;
            }
            else
            {
                randomPowerup++;
            }
        }
        Debug.Log("New Random is " + randomPowerup);
        GameObject activeGameObject = powerupPrefabs[randomPowerup];
        activeGameObject.transform.position = GenerateSpawnPos();
        activeGameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}

