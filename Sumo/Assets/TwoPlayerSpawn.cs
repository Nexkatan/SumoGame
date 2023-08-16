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

    public int player1Deaths = 0;
    public int player2Deaths = 0;


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
