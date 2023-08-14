using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidWall : MonoBehaviour
{
    public bool isAcid = false;
    public bool isBouncy = false;

    private float bounceForce = 3000;

    public Material acidMat;
    public ParticleSystem acidBubble;
    public PlayerController playerController;


    void Start()
    {
        
        isAcid = playerController.hasAcidPowerup;
    }

    private void Update()
    {
     
    }

    void OnCollisionEnter(Collision collision)
    {
        isBouncy = playerController.hasBouncyWallPowerup;
        if (isBouncy)
        {
            Rigidbody otherRb = collision.rigidbody;
            otherRb.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        }
         if (collision.gameObject.CompareTag("Enemy")) {
            
           
            if (isAcid)
            {
                Destroy(collision.gameObject);
            }
           
        }
    }
}
