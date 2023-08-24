
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject com;
    public Vector3 comOffset;
    public Animator playerAnim;

    public float impactPower = 1000;
    public float recoilPower = 200;

    public bool collided = false;

    public float gravityMod;
    public float horizRotateSpeed;
    public float verticRotateSpeed;
    public float jumpPower;

    public int deathCount = 0;
    public Vector3 respawnHeight = new Vector3(0, 5, 0);
    private bool death;
    private int spawnRange = 9;

    public CharGameManager charGameManager;

    public bool justJumped;
    public bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb.centerOfMass);
        rb.centerOfMass = comOffset;
        Debug.Log(rb.centerOfMass);
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityMod;
    }

    

    void OnCollisionEnter(Collision collision)
    {
        float currentVel = rb.velocity.magnitude;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collided)
            {
                Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 ForceDirection = collisionRb.transform.position - transform.position;
                collisionRb.AddForce(ForceDirection * currentVel * impactPower, ForceMode.Impulse);
                collided = true;
                Debug.Log("Collision");
                rb.AddRelativeForce(-Vector3.up * recoilPower, ForceMode.Impulse);
                StartCoroutine(collisionCountDown());
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            onGround = true;
            rb.centerOfMass = comOffset;
        }
    }

    IEnumerator collisionCountDown ()
    {
        yield return new WaitForSeconds(1);
        collided = false;
    }

    public void Die()
    {
       
        {
            deathCount++;
            Respawn();
            charGameManager.UpdateScore();
            death = true;
        }
    }

    public void Respawn()
    {

        Vector3 newPos = GenerateSpawnPos();
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        death = false;
        rb.transform.position = newPos + respawnHeight;
    }

    public Vector3 GenerateSpawnPos()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnRangeX, 0.1f, spawnRangeZ);
        return randomPos;
    }
}
