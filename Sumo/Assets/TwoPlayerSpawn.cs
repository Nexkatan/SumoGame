using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayerSpawn : SpawnManager
{
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public PlayerController player1;
    public PlayerController player2;


    void Start()
    {
        powerupRange = powerupPrefabs.Length;
        spawnPowerUp();
        StartCoroutine(TwoPlayerSpawnPowerup());
    }


    IEnumerator TwoPlayerSpawnPowerup () 
    {
        while (true) 
        { 
            yield return new WaitForSeconds(5);
            spawnPowerUp();
        }
    }
}
