using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInlaysTwoPlayer : MonoBehaviour
{
    public Player1Controller player1;
    public Player2Controller player2;



    public Vector3 respawnHeight = new Vector3(0, 5, 0);

    public Material acidMat;
    public Material bouncyMat;
    public Material teleMat;

    public float bouncePower = 2000;

    public SpawnManager spawnManager;
    public GameManager gameManager;

    private void Update()
    {
        if (gameManager.hasAcidPowerup)
        {
            GetComponent<MeshRenderer>().material = acidMat;
        }
        if (gameManager.hasBouncyWallPowerup)
        {
            GetComponent<MeshRenderer>().material = bouncyMat;
        }
        if (gameManager.hasTelePowerup)
        {
            GetComponent<MeshRenderer>().material = teleMat;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager.hasTelePowerup)
            {
                TeleOppositeWall(collision);
            }
            if (gameManager.hasBouncyWallPowerup)
            {
                collisionRb.AddExplosionForce(bouncePower, collision.contacts[0].point, 5);
            }
            if (gameManager.hasAcidPowerup)
            {
                Die(collision);
                Respawn(collision);
            }
        }
    }

    void TeleOppositeWall(Collision collision)
    {
        Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 currentVel = collisionRb.velocity;
        Vector3 oppositePos = Vector3.zero - new Vector3(collisionRb.transform.position.x, 0, collisionRb.transform.position.z) * 0.9f;
        collisionRb.transform.position = oppositePos;
        collisionRb.AddExplosionForce(currentVel.magnitude, oppositePos, 1);
    }

    public void Die(Collision collision)
    {
        BackToNormal(collision);
        collision.gameObject.GetComponent<PlayerController>().deathCount++;
        Debug.Log(collision.gameObject.GetComponent<PlayerController>().deathCount);
    }

    public void Respawn(Collision collision)
    {
        Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 newPos = spawnManager.GenerateSpawnPos();
        collisionRb.angularVelocity = Vector3.zero;
        collisionRb.velocity = Vector3.zero;
        collisionRb.transform.position = newPos + respawnHeight;
    }

    void BackToNormal(Collision collision)
    {
        Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
        collision.gameObject.GetComponent<PlayerController>().hasDoublePowerup = false;
        collision.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        collisionRb.mass = 1;
        collision.gameObject.GetComponent<PlayerController>().doubleMassSpeed = 1;
    }
}
