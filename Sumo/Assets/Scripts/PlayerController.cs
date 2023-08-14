using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameObject wallInlays;

    public ParticleSystem lightningSpark;

    public float jumpForce = 10;
    public float gravityModifier = 2;
    public float moveSpeed = 5;
    public float powerupStrength = 5;
    public float powerupTime = 7;
    private float jumpCount = 0;
    private Vector3 doubleSize = new Vector3(2, 2, 2);
    private float doubleMass = 10;

    public bool gameOver = false;

    public int numPowers = 5;
    public bool[] powerUpAlready;
    public bool hasImpactPowerup = false;
    public bool hasBalloonPowerup = false;
    public bool hasDoublePowerup = false;
    public bool hasAcidPowerup = false;
    public bool hasTeleportPowerup = false;
    public bool hasBouncyWallPowerup = false;
    public bool wavePUsed = true;


    void Start()
    {
        powerUpAlready = new bool[numPowers];
        powerUpAlready[0] = hasImpactPowerup;
        powerUpAlready[1] = hasBalloonPowerup;
        powerUpAlready[2] = hasDoublePowerup;
        powerUpAlready[3] = hasAcidPowerup;
        powerUpAlready[4] = hasTeleportPowerup;

        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        wallInlays = GameObject.Find("Wall Inlays");
        Physics.gravity = new Vector3(0, -9.8f, 0);
        Physics.gravity *= gravityModifier;
    }


    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * moveSpeed * verticalInput);
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * moveSpeed * horizontalInput);
        if (transform.position.y < -2)
        {
            SceneManager.LoadScene("Level 1");
        }
        if (Input.GetKeyDown(KeyCode.Space)) { 
            jump();
        }
        if (hasDoublePowerup)
        {
            transform.localScale = doubleSize;
            playerRb.mass = doubleMass;
            playerRb.AddForce(focalPoint.transform.right * moveSpeed * horizontalInput * doubleMass);
            playerRb.AddForce(focalPoint.transform.forward * moveSpeed * verticalInput * doubleMass);
        }
        else if (!hasDoublePowerup)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            playerRb.mass = 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        hidePowerup(other);
        if (other.CompareTag("ImpactPowerup"))
        {
            hasImpactPowerup = true;
            StartCoroutine("ImpactPowerDown");
        }
        else if (other.CompareTag("BalloonPowerup"))
        {
            hasBalloonPowerup = true;
            StartCoroutine("BalloonPowerDown");
        }
        else if (other.CompareTag("DoublePowerup"))
        {
            hasDoublePowerup = true;
            StartCoroutine("DoublePowerDown");
        }
        else if (other.CompareTag("AcidWallPowerup"))
        {
            hasAcidPowerup = true;
            StartCoroutine("AcidWallPowerDown");
        }
        else if (other.CompareTag("TeleportPowerup"))
        {
            hasTeleportPowerup = true;
            StartCoroutine("TeleportWallPowerDown");
        }
        else if (other.CompareTag("BouncyWallPowerup"))
        {
            hasBouncyWallPowerup = true;
            StartCoroutine("BouncyWallPowerDown");
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
                
                if (collision.gameObject.CompareTag("Enemy") && hasImpactPowerup)
            {
                lightningSpark.Play();
            }
                if (hasImpactPowerup) 
            { 
                playerRb.velocity *= 0.1f;
                enemyRigB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
                if (hasDoublePowerup)
            {
                enemyRigB.AddForce(awayFromPlayer * powerupStrength / 10, ForceMode.Impulse);
            } 
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
        if (collision.gameObject.CompareTag("WallInlay") && hasTeleportPowerup)
        {
            teleOppositeWall(collision);
            Debug.Log(collision.transform.position);
        }

    }

    void jump()
    {
        if (hasBalloonPowerup) {
            jumpCount++;
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            if (jumpCount < 2)
            {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            if (hasDoublePowerup && jumpCount < 2)
            {
            playerRb.AddForce(Vector3.up * jumpForce * 10, ForceMode.Impulse);
            }
        }
        else if (!hasBalloonPowerup) 
        {
            Debug.Log("Why still jump");
        }
    }


    void hidePowerup(Collider other)
    {
        wavePUsed = true;
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().transform.position = new Vector3(0, -5, 0);
    }
    


    void teleOppositeWall (Collision collision)
    {
        Vector3 currentVel = playerRb.velocity;
        Quaternion collideRotate = collision.transform.rotation;
        Vector3 oppositePos = Vector3.zero - new Vector3(playerRb.transform.position.x, 0, playerRb.transform.position.z) * 0.9f;
        playerRb.transform.position = oppositePos;
        playerRb.AddExplosionForce(currentVel.magnitude, oppositePos, 1);
        
    
    }

    IEnumerator ImpactPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasImpactPowerup = false;
    }
    IEnumerator BalloonPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasBalloonPowerup = false;
    }
    IEnumerator DoublePowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasDoublePowerup = false;
    }
    IEnumerator AcidWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasAcidPowerup = false;
    }
    IEnumerator BouncyWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasBouncyWallPowerup = false;
    }
    IEnumerator TeleportWallPowerDown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasTeleportPowerup = false;
    }
}
