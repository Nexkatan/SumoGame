using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;

    public GameObject WallInlayz;

    public ParticleSystem lightningSpark;

    public SpawnManager spawnManager;

    public GameManager gameManager;

    public Vector3 respawnHeight = new Vector3(0, 5, 0);

    public float gravityModifier = 2;
    public float moveSpeed = 1;
    public float impactStrength = 15;
    public float powerupTime = 7;
    public float bounceForce = 2000;
    public Vector3 doubleSize = new Vector3(2, 2, 2);
    public float doubleMass = 10;
    public float doubleMassSpeed = 1;

    public bool gameOver = false;
    public bool death = false;

    public int numPowers = 5;

    public bool hasBalloonPowerup = false;
    public bool hasDoublePowerup = false;
    public bool hasTelePowerup = false;
    public bool hasAcidPowerup = false; 
    public bool hasBouncyWallPowerup = false;

    public bool wavePUsed;

    public int deathCount = 0;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.8f, 0);
        Physics.gravity *= gravityModifier;
    }

    void FixedUpdate()
    {
        if (playerRb.position.y < -2)
        {
                
                Die();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        HidePowerup(other);

        if (other.CompareTag("BalloonPowerup"))
        {
            wavePUsed = true;
            hasBalloonPowerup = true;
            hasDoublePowerup = true;
            StartCoroutine("BalloonPowerDown");
        }

        if (other.CompareTag("DoublePowerup"))
        {
            wavePUsed = true;
            transform.localScale = doubleSize;
            playerRb.mass = doubleMass;
            doubleMassSpeed = 10;
            hasDoublePowerup = true;
            StartCoroutine("DoublePowerDown");
        }

        else if (other.CompareTag("AcidWallPowerup"))
        {
            if (WallInlayz.activeInHierarchy)
            {
                StopAllCoroutines();
                gameManager.hasTelePowerup = false;
                gameManager.hasBouncyWallPowerup = false;
            }
            wavePUsed = true;
            gameManager.hasAcidPowerup = true;
            WallInlayz.SetActive(true);
            StartCoroutine("AcidWallPowerDown");
        }
        else if (other.CompareTag("TeleportPowerup"))
        {
            if (WallInlayz.activeInHierarchy)
            {
                StopAllCoroutines();
                gameManager.hasAcidPowerup = false;
                gameManager.hasBouncyWallPowerup = false;
            }
            wavePUsed = true;
            gameManager.hasTelePowerup = true;
            WallInlayz.SetActive(true);
            StartCoroutine("TeleportWallPowerDown");
        }
        else if (other.CompareTag("BouncyWallPowerup"))
        {
            if (WallInlayz.activeInHierarchy)
            {
                StopAllCoroutines();
                gameManager.hasTelePowerup = false;
                gameManager.hasAcidPowerup = false;
            }
            wavePUsed = true;
            gameManager.hasBouncyWallPowerup = true;
            WallInlayz.SetActive(true);
            StartCoroutine("BouncyWallPowerDown");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lightningSpark.Play();
            Rigidbody enemyRigB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRigB.AddForce(awayFromPlayer * impactStrength, ForceMode.Impulse);
            if (hasDoublePowerup)
            {
                enemyRigB.AddForce(awayFromPlayer * impactStrength / 10, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            if (hasDoublePowerup)
            {
                enemyRigB.AddForce(awayFromPlayer * impactStrength / 10, ForceMode.Impulse);
            }
        }
    }

    void HidePowerup(Collider other)
    {
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().transform.position = new Vector3(0, -5, 0);
    }

    public void Die()
    {
        if (!gameManager.gameOver)
        {
            deathCount++;
            Respawn();
            gameManager.UpdateScore();
            BackToNormal();
            death = true;
        }
    }

    public void Respawn()
    {
        
            Vector3 newPos = spawnManager.GenerateSpawnPos();
            playerRb.angularVelocity = Vector3.zero;
            playerRb.velocity = Vector3.zero;
            death = false;
            playerRb.transform.position = newPos + respawnHeight;
    }

    IEnumerator DoublePowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        BackToNormal();
    }

    void BackToNormal()
    {
        hasDoublePowerup = false;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        playerRb.mass = 1;
        doubleMassSpeed = 1;
    }

    IEnumerator AcidWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        gameManager.hasAcidPowerup = false;
        WallInlayz.SetActive(false);
    }
    IEnumerator BouncyWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        gameManager.hasBouncyWallPowerup = false;
        WallInlayz.SetActive(false);
    }
    IEnumerator TeleportWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        gameManager.hasTelePowerup = false;
        WallInlayz.SetActive(false);
    }
    IEnumerator BalloonPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasBalloonPowerup = false;
    }
}