using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyMoveSpeed = 1;
    private int randomPlayer;
    private GameObject player;
    private Rigidbody enemyRb;

    public PlayerController playerScript;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        var homePlayers = GameObject.FindGameObjectsWithTag("Player");
        randomPlayer = Random.Range(0, homePlayers.Length);
        player = homePlayers[randomPlayer];
    }

    void Update()
    {
        Vector3 lookDirection = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
        enemyRb.AddForce(lookDirection * enemyMoveSpeed);
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
}
