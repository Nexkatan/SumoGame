using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyMoveSpeed = 1;
    private GameObject player;
    private Rigidbody enemyRb;

    public OnePlayerController playerScript;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
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
