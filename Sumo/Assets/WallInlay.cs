using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInlay : MonoBehaviour
{
    public OnePlayerController player;
    

    public Material acidMat;
    public Material bouncyMat;
    public Material teleMat;

    private void Update()
    {
        if (player.hasAcidPowerup)
        {
            GetComponent<MeshRenderer>().material = acidMat;
        }
        if (player.hasBouncyWallPowerup)
        {
            GetComponent<MeshRenderer>().material = bouncyMat;
        }
        if (player.hasTelePowerup)
        {
            GetComponent<MeshRenderer>().material = teleMat;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (player.hasBouncyWallPowerup)
            {
                collisionRb.AddExplosionForce(player.bounceForce, collision.contacts[0].point, 5);
            }
            if (player.hasAcidPowerup)
            {
                Destroy(collision.gameObject);
            }

        }
    }

}
        

