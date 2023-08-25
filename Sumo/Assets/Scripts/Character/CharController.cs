
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    public GameObject com;
    public Vector3 comOffset = new Vector3(0, -2f, 0);
    [HideInInspector] public Animator playerAnim;

    [HideInInspector] public float impactPower = 100;
    public GameObject impactPos;
    public float spinningAtkPower = 10;

    [HideInInspector] public bool collided = false;

    [HideInInspector] public float horizRotateSpeed = 500;
    [HideInInspector] public float verticRotateSpeed = 150;
    [HideInInspector] public float jumpPower = 1000;

    [HideInInspector] public float velocity;

    [HideInInspector] public int deathCount = 0;
    [HideInInspector] public Vector3 respawnHeight = new Vector3(0, 5, 0);
    [HideInInspector] public bool death;
    private int spawnRange = 9;

    public CharGameManager charGameManager;
    private int numPlayers;

    [HideInInspector] public bool justJumped;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool isSpinning;
    [HideInInspector] public int slowDownPerSecond = 100;

    void Start()
    {
        numPlayers = GameObject.FindObjectsOfType<CharController>().Length;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = comOffset;
        playerAnim = GetComponent<Animator>();
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            rb.centerOfMass = comOffset;
        }

        if (!collided)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Rigidbody enemyRigB = collision.gameObject.GetComponent<Rigidbody>();
                collided = true;

                Vector3 awayFromPlayer = (collision.gameObject.transform.position - impactPos.gameObject.transform.position).normalized;

                if (isSpinning && onGround)
                {
                    enemyRigB.AddForce(awayFromPlayer * impactPower * spinningAtkPower, ForceMode.Impulse);
                    Debug.Log("Spinning attack");
                    StartCoroutine(collisionCountDown(1));
                }
                else
                {
                    enemyRigB.AddForce(awayFromPlayer * impactPower, ForceMode.Impulse);
                    StartCoroutine(collisionCountDown(2));
                }
            }
        }
    }

    public IEnumerator collisionCountDown(int inty)
    {
        yield return new WaitForSeconds(inty);
        collided = false;
    }

    public void Die()
    {
        {
            deathCount++;
            Respawn();
            if (charGameManager != null) 
            {
                charGameManager.UpdateScore();
            }
            
            death = true;
        }
    }

    public void Respawn()
    {
        Vector3 newPos = GenerateSpawnPos();
        float randomRotate = UnityEngine.Random.Range(0, 360);
        rb.transform.rotation = Quaternion.AngleAxis(randomRotate, Vector3.up);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        death = false;
        rb.transform.position = newPos + respawnHeight;
    }

    public Vector3 GenerateSpawnPos()
    {
        float spawnRangeX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnRangeX, 0.1f, spawnRangeZ);
        return randomPos;
    }
}
